using Instruction_Panels;
using UnityEngine;

namespace Conditions
{
    public abstract class ConditionManager : MonoBehaviour
    {
        public void Activate(PanelData panelData)
        {
            OnActivated(panelData);
        }

        public void Deactivate()
        {
            OnDeactivated();
        }

        protected InstructionPanel GetPanelPrefab(PanelType panelType) =>
            SceneController.Instance.GetPanelPrefab(panelType);

        protected virtual void OnActivated(PanelData panelData)
        {
        }

        protected virtual void OnDeactivated()
        {
        }
    }
}