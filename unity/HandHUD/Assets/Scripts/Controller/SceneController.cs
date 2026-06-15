using System;
using System.Collections;
using Conditions;
using Instruction_Panels;
using Study;
using UnityEngine;

using static Study.StudyConfig;

public class SceneController : MonoBehaviour
{
    [SerializeField] private InstructionPanel imageAndTextPanel;
    [SerializeField] private InstructionPanel imageOnlyPanel;
    [SerializeField] private InstructionPanel textOnlyPanel;

    [SerializeField] private ConditionManager faManager;
    [SerializeField] private ConditionManager hpManager;
    [SerializeField] private ConditionManager waManager;

    private Participant participant;

    public static SceneController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    public void SwitchCondition(Condition condition)
    {
        waManager.Deactivate();
        faManager.Deactivate();
        hpManager.Deactivate();

        switch (condition)
        {
            case Condition.WorldAnchored:
                waManager.Activate(StudyConfig.GetPanelData(0));
                break;

            case Condition.ForearmAnchored:
                faManager.Activate(StudyConfig.GetPanelData(0));
                break;

            case Condition.HandProximal:
                hpManager.Activate(StudyConfig.GetPanelData(0));
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
}