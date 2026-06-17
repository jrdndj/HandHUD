using System;
using System.Collections;
using Conditions;
using Instruction_Panels;
using Questionnaire;
using Study;
using UnityEngine;

namespace Controller
{
    public enum StudyState
    {
        RunningTask,
        ConditionQuestionnaire,
        Finished
    }

    public class TaskController : MonoBehaviour
    {
        [SerializeField] private InstructionPanel imageAndTextPanel;
        [SerializeField] private InstructionPanel imageOnlyPanel;
        [SerializeField] private InstructionPanel textOnlyPanel;

        [SerializeField] private ConditionManager faManager;
        [SerializeField] private ConditionManager hpManager;
        [SerializeField] private ConditionManager waManager;

        [SerializeField] private Transform questionnaireParent;
        [SerializeField] private QuestionnaireManager preliminaryQuestionnairePrefab;
        [SerializeField] private QuestionnaireManager conditionQuestionnairePrefab;

        private StudyDataCollector _studyDataCollector;
        private Participant _participant;

        private int _currentConditionIdx = 0;
        private int _currentTaskIdx = 0;
        private int _currentSuperBlockIdx = 0;

        public static TaskController Instance;

        private StudyState _state = (StudyState)(-1);

        public event Action<int, int, int> OnTaskComplete;
        public event Action<StudyState> OnStateChanged;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (StudyConfig.Invalid)
            {
                SetState(StudyState.Finished);
                return;
            }

            StartCoroutine(EnterPreliminaryQuestionnaire()); // entry point :3
        }

        private void SetState(StudyState state)
        {
            if (_state == state)
                return;

            _state = state;
            OnStateChanged?.Invoke(state);
        }

        public void CompleteCurrentTask()
        {
            if (_state is StudyState.Finished or StudyState.ConditionQuestionnaire) return;

            _studyDataCollector.RecordTask(_currentTaskIdx, TaskTracker.TimerEnd());
            OnTaskComplete?.Invoke(_currentSuperBlockIdx, _currentConditionIdx, _currentTaskIdx);

            if (_currentTaskIdx + 1 < StudyConfig.TasksPerCondition)
            {
                _currentTaskIdx++;

                PanelData panelData = StudyConfig.GetPanelData(_currentTaskIdx);
                UpdateCurrentPanel(panelData);

                TaskTracker.TimerStart();

                return;
            }

            EnterConditionQuestionnaire();
        }

        private IEnumerator EnterPreliminaryQuestionnaire()
        {
            yield return new WaitForSeconds(2); // sometimes it loads too early

            SetState(StudyState.ConditionQuestionnaire);
            SwitchCondition(Condition.None);

            // instantiate questionnaire and goto next condition after its done
            QuestionnaireManager questionnaire = Instantiate(preliminaryQuestionnairePrefab, questionnaireParent);
            questionnaire.GetComponent<QuestionnaireController>().OnQuestionnaireCompleted += (result) =>
                OnPreliminaryQuestionnaireFinished(questionnaire, result);
        }

        private void EnterConditionQuestionnaire()
        {
            SetState(StudyState.ConditionQuestionnaire);
            SwitchCondition(Condition.None);

            // instantiate questionnaire and goto next condition after its done
            QuestionnaireManager questionnaire = Instantiate(conditionQuestionnairePrefab, questionnaireParent);
            questionnaire.OnSequenceFinished += () => OnConditionQuestionnaireFinished(questionnaire);
            questionnaire.GetComponent<QuestionnaireController>().OnQuestionnaireCompleted +=
                _studyDataCollector.OnQuestionnaireCompleted;
        }

        private void OnPreliminaryQuestionnaireFinished(QuestionnaireManager questionnaire, QuestionnaireResult result)
        {
            _participant =
                new Participant((int)result.responses[0].score +
                                1); // first questionnaire has only 1 question which is participantNo (0-based)
            _studyDataCollector = new StudyDataCollector(_participant);

            Destroy(questionnaire.gameObject);

            if (_state == StudyState.Finished)
                return;

            EnterCondition();
        }

        private void OnConditionQuestionnaireFinished(QuestionnaireManager questionnaire)
        {
            Destroy(questionnaire.gameObject);

            AdvanceCondition();

            if (_state == StudyState.Finished)
                return;

            EnterCondition();
        }

        private void AdvanceCondition()
        {
            _currentTaskIdx = 0;

            _currentConditionIdx++;

            if (_currentConditionIdx < StudyConfig.ConditionsPerSuperBlock)
                return;

            _currentConditionIdx = 0;
            AdvanceSuperBlock();
        }

        private void AdvanceSuperBlock()
        {
            if (_state is StudyState.Finished) return;

            _currentSuperBlockIdx++;
            if (_currentSuperBlockIdx < StudyConfig.SuperBlockCount) return;

            SetState(StudyState.Finished);
            _studyDataCollector.Export();
        }

        private void EnterCondition()
        {
            SetState(StudyState.RunningTask);

            Condition condition = StudyConfig.GetCondition(_participant.LatinSquareGroup, _currentConditionIdx);
            SwitchCondition(condition);

            PanelData panelData = StudyConfig.GetPanelData(_currentTaskIdx);
            UpdateCurrentPanel(panelData);

            _studyDataCollector.SetContext(GetCurrentSuperBlock(), condition);
            TaskTracker.TimerStart();
        }

        public void SwitchCondition(Condition condition)
        {
            waManager.Deactivate();
            faManager.Deactivate();
            hpManager.Deactivate();

            switch (condition)
            {
                case Condition.WorldAnchored:
                    waManager.Activate();
                    break;

                case Condition.ForearmAnchored:
                    faManager.Activate();
                    break;

                case Condition.HandProximal:
                    hpManager.Activate();
                    break;

                case Condition.None:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
            }
        }

        public void UpdateCurrentPanel(PanelData panelData)
        {
            if (panelData != null)
                GetCurrentManager().SetInstructionPanel(panelData);
        }

        public InstructionPanel GetPanelPrefab(PanelType panelType)
        {
            return panelType switch
            {
                PanelType.ImageOnly => imageOnlyPanel,
                PanelType.TextOnly => textOnlyPanel,
                PanelType.ImageAndText => imageAndTextPanel,
                _ => throw new ArgumentOutOfRangeException(nameof(panelType), "invalid panelType")
            };
        }

        private ConditionManager GetCurrentManager()
        {
            return StudyConfig.GetCondition(_participant.LatinSquareGroup, _currentConditionIdx) switch
            {
                Condition.WorldAnchored => waManager,
                Condition.ForearmAnchored => faManager,
                Condition.HandProximal => hpManager,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private SuperBlock GetCurrentSuperBlock()
        {
            return _currentSuperBlockIdx == 0
                ? _participant.SuperBlockGroup
                : _participant.SuperBlockGroup == SuperBlock.Glove
                    ? SuperBlock.NoGlove
                    : SuperBlock.Glove;
        }
    }
}