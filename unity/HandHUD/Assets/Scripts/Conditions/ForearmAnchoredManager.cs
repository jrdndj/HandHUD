using Instruction_Panels;
using Utility;

namespace Conditions
{
    public class ForearmAnchoredManager : ConditionManager
    {
        private InstructionPanel _instance;

        protected override void OnDeactivated()
        {
            if (!_instance) return;

            Destroy(_instance.gameObject);
            _instance = null;
        }

        public override void SetInstructionPanel(PanelData panelData)
        {
            if (_instance)
                Destroy(_instance.gameObject);

            InstructionPanel prefab = GetPanelPrefab(panelData.panelType);
            _instance = Instantiate(prefab, transform, false);
            _instance.GetComponent<StickToForearm>().enabled = true;
            _instance.UpdateData(panelData);
        }
    }
}
