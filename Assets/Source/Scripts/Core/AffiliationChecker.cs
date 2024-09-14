using BugStrategy.Unit;

namespace BugStrategy
{
    public static class AffiliationChecker
    {
        /// <returns>
        ///     return true if myAffiliation != other && != Neutral && != None
        /// </returns>
        public static bool CheckEnemies(this AffiliationEnum myAffiliation, AffiliationEnum other)
            => myAffiliation != other && other != AffiliationEnum.Neutral && other != AffiliationEnum.None;
    }
}