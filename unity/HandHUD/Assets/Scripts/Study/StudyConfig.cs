using System;
using Instruction_Panels;
using UnityEngine;
using static Study.Condition;

namespace Study
{
    public enum SuperBlock
    {
        NoGloveFirst,
        GloveFirst,
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
            new (new Color32(139, 125, 186, 255), "Connect the color-coded wires to the device",
                PanelType.ImageAndText, null // TODO
            ),
            new(new Color32(95, 168, 211, 255),
                "Use the tweezers to build the shape using the colored blocks",
                PanelType.ImageAndText,
                null // TODO
            ),
            new(new Color32(120, 198, 163, 255),
                "Rotate the valve fully clockwise then counter-clockwise",
                PanelType.ImageAndText,
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
            if (idx < 0 || idx >= TasksPerCondition)
                throw new ArgumentOutOfRangeException(nameof(idx), "idx out of range.");
            return TaskList[idx];
        }

        public static Condition GetCondition(Participant participant, int conditionIdx)
        {
            if (conditionIdx < 0 || conditionIdx >= ConditionsPerSuperBlock)
                throw new ArgumentOutOfRangeException(nameof(conditionIdx), "conditionIdx out of range.");
            return ConditionOrders[participant.LatinSquareGroup][conditionIdx];
        }
    }
}