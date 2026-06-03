using System;
using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public enum Condition
    {
        WorldAnchored,
        ForearmAnchored,
        HandProximal,
        TabletBaseline, // not sure what to do with this yet
    }

    public class ContextManager : MonoBehaviour
    {
        [SerializeField] private MarkerDatabase markerDatabase;

        [SerializeField] private InstructionPanel imageAndTextPanel;
        [SerializeField] private InstructionPanel imageOnlyPanel;
        [SerializeField] private InstructionPanel textOnlyPanel;

        [SerializeField] private ConditionManagerBase faManager;
        [SerializeField] private ConditionManagerBase hpManager;
        [SerializeField] private ConditionManagerBase waManager;

        private Condition _currentCondition;
        private MarkerData _currentMarker;

        public static ContextManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (markerDatabase.markers.Count <= 0)
                throw new InvalidOperationException("MarkerDatabase is empty. Add at least one MarkerData entry.");

            SetCurrentMarker(0);

            _currentCondition = Condition.WorldAnchored;
            SwitchCondition(_currentCondition);
            
            // StartCoroutine(ConditionCycleRoutine());
        }

        private IEnumerator ConditionCycleRoutine()
        {
            int mId = 0;
            
            while (true)
            {
                // Debug.Log("SWITCHING TO WORLD-ANCHORED");
                // _currentCondition = Condition.WorldAnchored;
                // SwitchCondition(_currentCondition);
                // yield return new WaitForSeconds(10f);

                Debug.Log("SWITCHING TO FOREARM-ANCHORED");
                _currentCondition = Condition.ForearmAnchored;
                SwitchCondition(_currentCondition);
                yield return new WaitForSeconds(10f);
                
                Debug.Log("SWITCHING TO HAND-PROXIMAL");
                _currentCondition = Condition.HandProximal;
                SwitchCondition(_currentCondition);
                yield return new WaitForSeconds(10f);

                SetCurrentMarker(++mId);
            }
        }

        public void SwitchCondition(Condition condition)
        {
            // TODO: remove this is only for testing
            SetCurrentMarker(Random.Range(0, 4));
            
            waManager.Deactivate();
            faManager.Deactivate();
            hpManager.Deactivate();

            switch (condition)
            {
                case Condition.WorldAnchored:
                    waManager.Activate(_currentMarker);
                    break;

                case Condition.ForearmAnchored:
                    faManager.Activate(_currentMarker);
                    break;

                case Condition.HandProximal:
                    hpManager.Activate(_currentMarker);
                    break;

                case Condition.TabletBaseline:
                    // none active
                    break;

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

        public MarkerData GetMarkerData(int id)
        {
            if (id < 0 || id >= markerDatabase.markers.Count)
                return MarkerData.Invalid;

            return markerDatabase.markers[id];
        }

        public void SetCurrentMarker(int id)
        {
            _currentMarker = GetMarkerData(id);
        }
    }
}