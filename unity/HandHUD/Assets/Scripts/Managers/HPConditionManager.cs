using Core;
using UnityEngine;
using Utility;

namespace Managers
{
    public class HPConditionManager : ConditionManagerBase
    {
        private bool _disabled = true;
        private InstructionPanel _instance;
        
        protected override void OnActivated()
        {
            _disabled = false;
            var prefab = GetPanelPrefab(MarkerData.panelType);
            _instance = Instantiate(prefab, transform, false);
            
            _instance.GetComponent<FloatNearHand>().enabled = true;
            _instance.GetComponent<FaceCamera>().enabled = true;
            
            _instance.UpdateData(MarkerData);
        }
        
        protected override void OnDeactivated()
        {
            if (_disabled) return;
            _disabled = true;
            Destroy(_instance.gameObject);
        }
    }
}