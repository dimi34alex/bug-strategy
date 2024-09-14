namespace BugStrategy.Libs
{
    public static class CheckOnNullExstension
    {
        /// <summary>
        /// Method for checking interfaces, that can be realized in class that inherited from UnityEngine.Object, for null
        /// </summary>
        /// <returns>
        /// return true if value is c# null or Unity null, else return false
        /// </returns>
        public static bool IsAnyNull<T>(this T value) 
            => value == null || ((value is UnityEngine.Object) && (value as UnityEngine.Object) == null);
    }
}