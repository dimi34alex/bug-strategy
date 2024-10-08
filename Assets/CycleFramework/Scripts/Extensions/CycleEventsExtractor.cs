﻿using System;
using System.Reflection;
using CycleFramework.Bootload;
using CycleFramework.Execute;

namespace CycleFramework.Extensions
{
    public static class CycleEventsExtractor
    {
        private static readonly BindingFlags _methodFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private static readonly Type _initializerType = typeof(CycleInitializerBase);

        public static MethodInfo ExtractSpecificEventInfo(CycleMethodType cycleMethodType)
        {
            return _initializerType.GetMethod(cycleMethodType.ToString(), _methodFlag);
        }
    }
}
