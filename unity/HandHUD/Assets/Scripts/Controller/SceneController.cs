using System;
using Conditions;
using Instruction_Panels;
using Study;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private InstructionPanel imageAndTextPanel;
    [SerializeField] private InstructionPanel imageOnlyPanel;
    [SerializeField] private InstructionPanel textOnlyPanel;

    [SerializeField] private ConditionManager faManager;
    [SerializeField] private ConditionManager hpManager;
    [SerializeField] private ConditionManager waManager;

    private Participant _participant;
    private int _currentConditionIdx = 0;
    private int _currentTaskIdx = 0;
    private int _currentSuperBlockIdx = 0;

    public static TaskController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SwitchCondition(StudyConfig.GetCondition(_participant, _currentConditionIdx));
        GetCurrentManager().SetInstructionPanel(StudyConfig.GetPanelData(_currentTaskIdx));
    }

    public void ProceedToNextTask()
    {
        _currentTaskIdx++;
        if (_currentTaskIdx >= StudyConfig.TasksPerCondition)
        {
            ProceedToNextCondition();
        }
        else
        {
            GetCurrentManager().SetInstructionPanel(StudyConfig.GetPanelData(_currentTaskIdx));
        }
    }

    private void ProceedToNextCondition()
    {
        _currentConditionIdx++;
        if (_currentConditionIdx >= StudyConfig.ConditionsPerSuperBlock)
        {
            ProceedToNextSuperBlock();
        }
        else
        {
            _currentTaskIdx = 0;
            SwitchCondition(StudyConfig.GetCondition(_participant, _currentConditionIdx));
        }
    }

    private void ProceedToNextSuperBlock()
    {
        _currentSuperBlockIdx++;
        if (_currentSuperBlockIdx >= StudyConfig.SuperBlockCount)
        {
            // TODO: done
        }
        else
        {
            _currentTaskIdx = 0;
            _currentConditionIdx = 0;
            SwitchCondition(StudyConfig.GetCondition(_participant, _currentConditionIdx));
        }
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

            // case Condition.TabletBaseline:
            //     // none active
            //     break;

            default:
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
        }
    }

    public InstructionPanel GetPanelPrefab(PanelType panelType)
    {
        return panelType switch
        {
            PanelType.ImageOnly => imageOnlyPanel,
            PanelType.TextOnly => textOnlyPanel,
            PanelType.ImageAndText => imageAndTextPanel,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private ConditionManager GetCurrentManager()
    {
        switch (StudyConfig.GetCondition(_participant, _currentConditionIdx))
        {
            case Condition.WorldAnchored:
                return waManager;
            case Condition.ForearmAnchored:
                return faManager;
            case Condition.HandProximal:
                return hpManager;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}