using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        NoMoreConditions,
    }

    public static class StudyConfig
    {
        // number of blocks: with glove, without glove
        public const int SuperBlockCount = 2;

        public static int LatinSquareGroupCount => ConditionOrders.Length;

        public static int ConditionsPerSuperBlock => ConditionOrders[0].Length;

        public static int TasksPerCondition => TaskList.Length;

        public static PanelData GetPanelData(int idx)
        {
            if (idx < 0 || idx >= TasksPerCondition)
                throw new ArgumentOutOfRangeException("idx out of range.");
            return TaskList[idx];
        }

        public static Condition GetCondition(Participant participant, int conditionIdx)
        {
            if (conditionIdx < 0 || conditionIdx >= ConditionsPerSuperBlock)
                throw new ArgumentOutOfRangeException("conditionIdx out of range.");
            return ConditionOrders[participant.LatinSquareGroup][conditionIdx];
        }

        public static readonly Condition[][] ConditionOrders =
        {
            new[] { WorldAnchored, ForearmAnchored, HandProximal },
            new[] { ForearmAnchored, HandProximal, WorldAnchored },
            new[] { HandProximal, WorldAnchored, ForearmAnchored }
        };

        public static readonly PanelData[] TaskList =
        {
            new()
            {
                color = Color.green,
                message = "Message 1",
                panelType = PanelType.ImageAndText,
                sprite = null // TODO
            },
            new()
            {
                color = Color.orange,
                message = "Message 2",
                panelType = PanelType.ImageAndText,
                sprite = null // TODO
            },
            new()
            {
                color = Color.purple,
                message = "Message 3",
                panelType = PanelType.ImageAndText,
                sprite = null // TODO
            },
            new()
            {
                color = Color.yellow,
                message = "Message 4",
                panelType = PanelType.ImageAndText,
                sprite = null // TODO
            },
        };
    }
}