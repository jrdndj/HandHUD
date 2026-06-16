using System;
using Conditions;
using Instruction_Panels;
using Questionnaire;
using Study;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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

        private Participant _participant;
        private int _currentConditionIdx = 0;
        private int _currentTaskIdx = 0;
        private int _currentSuperBlockIdx = 0;

        public static TaskController Instance;

        private StudyState _state = (StudyState)(-1);

        public TaskController(QuestionnaireManager preliminaryQuestionnairePrefab)
        {
            this.preliminaryQuestionnairePrefab = preliminaryQuestionnairePrefab;
        }

        public static event Action<int, int, int> OnTaskComplete;
        public static event Action<StudyState> OnStateChanged;

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

            // TODO: dont make participant here
            _participant = new Participant(0, SuperBlock.GloveFirst);

            EnterCondition();
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

            if (_currentTaskIdx + 1 < StudyConfig.TasksPerCondition)
            {
                _currentTaskIdx++;

                PanelData panelData = StudyConfig.GetPanelData(_currentTaskIdx);
                UpdateCurrentPanel(panelData);

                OnTaskComplete?.Invoke(_currentSuperBlockIdx, _currentConditionIdx, _currentTaskIdx);
                return;
            }

            EnterConditionQuestionnaire();
        }

        private void EnterConditionQuestionnaire()
        {
            SetState(StudyState.ConditionQuestionnaire);

            SwitchCondition(Condition.None);

            QuestionnaireManager questionnaire = Instantiate(conditionQuestionnairePrefab, questionnaireParent);
            questionnaire.OnSequenceFinished += () => OnConditionQuestionnaireFinished(questionnaire);
        }

        private void OnConditionQuestionnaireFinished(QuestionnaireManager manager)
        {
            Destroy(manager.gameObject);

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
            _currentSuperBlockIdx++;
            if (_currentSuperBlockIdx < StudyConfig.SuperBlockCount) return;

            SetState(StudyState.Finished);
        }

        private void EnterCondition()
        {
            SetState(StudyState.RunningTask);

            Condition condition = StudyConfig.GetCondition(_participant, _currentConditionIdx);
            SwitchCondition(condition);

            PanelData panelData = StudyConfig.GetPanelData(_currentTaskIdx);
            UpdateCurrentPanel(panelData);
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
            return StudyConfig.GetCondition(_participant, _currentConditionIdx) switch
            {
                Condition.WorldAnchored => waManager,
                Condition.ForearmAnchored => faManager,
                Condition.HandProximal => hpManager,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}