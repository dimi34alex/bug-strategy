namespace Source.Scripts.Missions
{
    public static class GlobalDataHolder
    {
        public static GlobalData GlobalData { get; private set; } = new();

        public static void Load() 
            => GlobalData = SerializeExtensions.Deserialize<GlobalData>() ?? new GlobalData();

        public static void Save() 
            => SerializeExtensions.Serialize(GlobalData);
    }
}