namespace Source.Scripts.Missions
{
    public static class GlobalDataHolder
    {
        private static GlobalData _globalData = new();
        public static GlobalData GlobalData => _globalData;

        public static void Load()
        {
            _globalData = SerializeExtensions.Deserialize<GlobalData>() ?? new GlobalData();
        }

        public static void Save()
        {
            SerializeExtensions.Serialize(_globalData);
        }
    }
}