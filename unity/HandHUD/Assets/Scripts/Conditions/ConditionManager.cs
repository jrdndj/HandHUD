using Controller;
using Instruction_Panels;
using UnityEngine;

namespace Conditions
{
    public abstract class ConditionManager : MonoBehaviour
    {
        public void Activate()
        {
            OnActivated();
        }

        public void Deactivate()
        {
            OnDeactivated();
        }

        protected InstructionPanel GetPanelPrefab(PanelType panelType) =>
            TaskController.Instance.GetPanelPrefab(panelType);

        protected virtual void OnActivated()
        {
        }

        protected virtual void OnDeactivated()
        {
        }

        public virtual void SetInstructionPanel(PanelData panelData)
        {
        }
    }
}