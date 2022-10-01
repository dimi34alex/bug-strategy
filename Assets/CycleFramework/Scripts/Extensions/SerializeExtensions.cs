using UnityEngine;

public static class SerializeExtensions
{
    public static void Serialize<T>(T element)
    {
        string json = JsonUtility.ToJson(element);
        PlayerPrefs.SetString(element.GetType().FullName, json);
    }

    public static T Deserialize<T>()
    {
        string json = PlayerPrefs.GetString(typeof(T).FullName);

        if (string.IsNullOrEmpty(json))
            return default;

        return JsonUtility.FromJson<T>(json);
    }
}
