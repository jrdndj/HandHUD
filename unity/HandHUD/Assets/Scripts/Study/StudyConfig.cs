using System;
using Instruction_Panels;
using UnityEngine;
using static Study.Condition;

namespace Study
{
    public enum SuperBlock
    {
        NoGlove,
        Glove,
    }

    public enum Condition
    {
        WorldAnchored,
        ForearmAnchored,
        HandProximal,

        None,

        // TabletBaseline, // not sure what to do with this yet
    }

    public static class StudyConfig
    {
        private static readonly Condition[][] ConditionOrders =
        {
            new[] { WorldAnchored, ForearmAnchored, HandProximal },
            new[] { ForearmAnchored, HandProximal, WorldAnchored },
            new[] { HandProximal, WorldAnchored, ForearmAnchored }
        };

        private static readonly PanelData[] TaskList =
        {
            new(new Color32(139, 125, 186, 255), "Connect the color-coded wires to the device",
                PanelType.TextOnly, null // TODO
            ),
            new(new Color32(95, 168, 211, 255),
                "Use the tweezers to remove the blocks and place them in the right color slot",
                PanelType.TextOnly,
                null // TODO
            ),
            new(new Color32(120, 198, 163, 255),
                "Rotate the valve 360 degrees until the sign is aligned with the colored line",
                PanelType.TextOnly,
                null // TODO
            ),
        };

        // number of blocks: with glove, without glove
        public static readonly int SuperBlockCount = Enum.GetValues(typeof(SuperBlock)).Length;
        public static int LatinSquareGroupCount => ConditionOrders.Length;
        public static int ConditionsPerSuperBlock => ConditionOrders.Length > 0 ? ConditionOrders[0].Length : 0;
        public static int TasksPerCondition => TaskList.Length;

        public static readonly bool Invalid = TasksPerCondition <= 0 || ConditionsPerSuperBlock <= 0 ||
                                              LatinSquareGroupCount <= 0 || SuperBlockCount <= 0;

        public static PanelData GetPanelData(int idx)
        {
            if (idx >= 0 && idx < TasksPerCondition)
                return TaskList[idx];
            return PanelData.Invalid;
        }

        public static Condition GetCondition(int latinSquareGroup, int conditionIdx)
        {
            if (conditionIdx < 0 || conditionIdx >= ConditionsPerSuperBlock)
                throw new ArgumentOutOfRangeException(nameof(conditionIdx), "conditionIdx out of range.");
            return ConditionOrders[latinSquareGroup][conditionIdx];
        }
    }
}