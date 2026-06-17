using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Questionnaire;
using UnityEngine.Serialization;

namespace Study
{
    /* Export Shape:
       {
            "participantNo": 1,
            "superBlockGroup": "Glove",
            "latinSquareGroup": 0,
            "started": "...",
            "finished": "...",
            "blocks": [
                {
                    "superBlock": "Glove",
                    "condition": "WorldAnchored",
                    "tasks": [
                        { "taskNo": 1, "completionTimeSeconds": 12.3 }
                    ],
                    "questionnaires": [
                        {
                            "questionnaireName": "System Usability Scale",
                            "timestamp": "...",
                            "responses": [...]
                        }
                    ]
                }
            ]
       }
     */

    [Serializable]
    public class StudyDataCollector
    {
        private readonly ParticipantExport _data;

        private SuperBlock _currentSuperBlock;
        private Condition _currentCondition;

        public StudyDataCollector(Participant participant)
        {
            _data = new ParticipantExport(participant);
        }

        public void SetContext(SuperBlock superBlock, Condition condition)
        {
            _currentSuperBlock = superBlock;
            _currentCondition = condition;
        }

        public void RecordTask(int taskNo, double completionTime)
        {
            var block = _data.GetOrCreateBlock(_currentSuperBlock, _currentCondition);
            block.AddTask(taskNo, completionTime);
        }

        public void Export()
        {
            DateTime now = DateTime.Now;
            _data.finished = now.ToString("yyyy-MM-dd HH:mm:ss"); // record finish time

            string json = JsonConvert.SerializeObject(
                _data,
                Formatting.Indented
            );

            string path = Path.Combine(
                Application.persistentDataPath,
                $"{_data.participantNo}_{now:yyyyMMdd_HHmmss}.json"
            );

            File.WriteAllText(path, json);

            Debug.Log($"Exported to: {path}");
        }

        public void OnQuestionnaireCompleted(QuestionnaireResult result)
        {
            _data.GetOrCreateBlock(_currentSuperBlock, _currentCondition).questionnaires.Add(result);
        }
    }

    [Serializable]
    public class TaskExport
    {
        public int taskNo;
        public double completionTimeSeconds;

        public TaskExport(int taskNo, double completionTime)
        {
            this.taskNo = taskNo + 1; // 1-based
            this.completionTimeSeconds = completionTime;
        }
    }

    [Serializable]
    public class BlockExport
    {
        public string superBlock;
        public string condition;

        public List<TaskExport> tasks = new();
        public List<QuestionnaireResult> questionnaires = new();

        public BlockExport(SuperBlock superBlock, Condition condition)
        {
            this.superBlock = superBlock.ToString();
            this.condition = condition.ToString();
        }

        public void AddTask(int taskNo, double completionTime)
        {
            tasks.Add(new TaskExport(taskNo, completionTime));
        }
    }

    [Serializable]
    public class ParticipantExport
    {
        public string participantNo;
        public string superBlockGroup;
        public int latinSquareGroup;
        public string started;
        public string finished;

        public List<BlockExport> blocks = new();

        public ParticipantExport(Participant participant)
        {
            // 1-based indexing here
            participantNo = $"P{participant.ParticipantNo}";
            latinSquareGroup = participant.LatinSquareGroup + 1;
            superBlockGroup = participant.SuperBlockGroup.ToString();
            started = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public BlockExport GetOrCreateBlock(SuperBlock superBlock, Condition condition)
        {
            var block = blocks.Find(b =>
                b.superBlock == superBlock.ToString() &&
                b.condition == condition.ToString());

            if (block == null)
            {
                block = new BlockExport(superBlock, condition);
                blocks.Add(block);
            }

            return block;
        }
    }
}