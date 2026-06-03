using Core;
using UnityEngine;

namespace Managers
{
    public abstract class ConditionManagerBase : MonoBehaviour
    {
        protected MarkerData MarkerData;

        public void Activate(MarkerData markerData)
        {
            gameObject.SetActive(true);

            MarkerData = markerData;

            OnActivated();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);

            OnDeactivated();
        }

        protected InstructionPanel GetPanelPrefab(PanelType panelType) =>
            ContextManager.Instance.GetPanelPrefab(panelType);
        
        protected MarkerData GetMarkerData(int id) =>
            ContextManager.Instance.GetMarkerData(id);

        protected virtual void OnActivated()
        {
        }

        protected virtual void OnDeactivated()
        {
        }
    }
}