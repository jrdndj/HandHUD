using Instruction_Panels;
using Utility;

namespace Conditions
{
    public class ForearmAnchoredManager : ConditionManager
    {
        private bool _disabled = true;
        private InstructionPanel _instance;
        protected override void OnActivated(PanelData panelData)
        {
            _disabled = false;
            
            var prefab = GetPanelPrefab(panelData.panelType);
            _instance = Instantiate(prefab, transform, false);
            
            _instance.GetComponent<StickToForearm>().enabled = true;

            _instance.UpdateData(panelData);
        }

        protected override void OnDeactivated()
        {
            if (_disabled) return;
            _disabled = true;
            Destroy(_instance.gameObject);
        }
    }
}
