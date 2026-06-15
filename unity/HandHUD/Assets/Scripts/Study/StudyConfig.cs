using System;
using Instruction_Panels;
using UnityEngine;
using static Study.Condition;

namespace Study
{
    public enum SuperBlock
    {
        GloveFirst,
        NoGloveFirst
    }

    public enum Condition
    {
        WorldAnchored,
        ForearmAnchored,
        HandProximal,

        // TabletBaseline, // not sure what to do with this yet
    }

    public static class StudyConfig
    {
        // number of blocks: with glove, without glove
        public static readonly int SuperBlockCount = Enum.GetValues(typeof(SuperBlock)).Length;
        public static int LatinSquareGroupCount => ConditionOrders.Length;
        public static int ConditionsPerSuperBlock => ConditionOrders.Length > 0 ? ConditionOrders[0].Length : 0;
        public static int TasksPerCondition => TaskList.Length;

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

        private static readonly Condition[][] ConditionOrders =
        {
            new[] { WorldAnchored, ForearmAnchored, HandProximal },
            new[] { ForearmAnchored, HandProximal, WorldAnchored },
            new[] { HandProximal, WorldAnchored, ForearmAnchored }
        };

        private static readonly PanelData[] TaskList =
        {
            new(new Color(95, 168, 211),
                "Use the tweezers to build the shape using the colored blocks",
                PanelType.ImageAndText,
                null // TODO
            ),
            new(new Color(139, 125, 186),
                "Connect the color-coded wires to the device",
                PanelType.ImageAndText,
                null // TODO
            ),
            new(new Color(120, 198, 163),
                "Rotate the valve fully clockwise then counter-clockwise",
                PanelType.ImageAndText,
                null // TODO
            ),

            // new()
            // {
            //     color = Color.yellow,
            //     message = "Message 4",
            //     panelType = PanelType.ImageAndText,
            //     sprite = null // TODO
            // },
        };
    }
}