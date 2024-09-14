namespace CycleFramework.Extensions
{
    public static class CastExtensions
    {
        public static bool CastPossible<T>(this object value) => value is T;
        public static T Cast<T>(this object value) => (T)value;
        public static T UnSafeCast<T>(this object value) where T : class => value as T;
        public static bool TryCast<T>(this object value, out T castedValue)
        {
            if (value.CastPossible<T>())
            {
                castedValue = (T)value;
                return true;
            }
            else
            {
                castedValue = default;
                return false;
            }
        }
    }
}
