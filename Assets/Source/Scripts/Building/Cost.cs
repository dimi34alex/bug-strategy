using System.Collections.Generic;

public class Cost
{
    private Dictionary<ResourceID, int> _resourceCost;

    public Cost(KeyValuePair<ResourceID, int>[] keyValuePairs)
    {
        _resourceCost = new Dictionary<ResourceID, int>();

        foreach (var element in keyValuePairs)
            _resourceCost.Add(element.Key, element.Value);
    }

    public Cost(SerializableDictionary<ResourceID, int> keyValuePairs)
    {
        _resourceCost = new Dictionary<ResourceID, int>();

        foreach (var element in keyValuePairs)
            _resourceCost.Add(element.Key, element.Value);
    }
}