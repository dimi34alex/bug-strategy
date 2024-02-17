using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumValuesExtension
{
    public static class EnumValuesTool
    {
        /// <returns>
        /// return IEnumerable array of enum values
        /// </returns>
        public static IEnumerable<T> GetValues<T>() where T : Enum 
            =>  Enumerable.Cast<T>(Enum.GetValues(typeof(T)));
    }
}