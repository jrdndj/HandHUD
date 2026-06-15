using Instruction_Panels;
using Utility;

namespace Conditions
{
    public class ForearmAnchoredManager : ConditionManager
    {
        private InstructionPanel _instance;

        protected override void OnDeactivated()
        {
            Destroy(_instance.gameObject);
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
