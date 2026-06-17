using System;
using Instruction_Panels;
using Study;
using UnityEngine;

namespace Controller
{
    public static class TaskTracker
    {
        private static double _startTime;

        public static void TimerStart()
        {
            _startTime = Time.timeAsDouble;
        }

        public static double TimerEnd()
        {
            return Time.timeAsDouble - _startTime;
        }
    }
}