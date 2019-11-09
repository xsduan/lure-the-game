using UnityEngine;

/// <summary>
/// A specialized swapper that is used for for instantiating prefabs rather than using existing objects.
/// </summary>
public class UniquePrefabSwitch : Swapper<int, GameObject>
{
    /// <summary>
    /// Context under which new objects should be stored.
    /// </summary>
    private readonly Transform context;

    public UniquePrefabSwitch(Transform context) : base((go, active) => go?.SetActive(active))
    {
        this.context = context;
    }

    /// <summary>
    /// Same as CurrentKey, but bases it off of prefabs. This relies on the user being honest about if a GameObject is
    /// actually a prefab, but it shouldn't break or anything if you do use a normal GameObject.
    /// </summary>
    /// <param name="prefab">Template object to instantiate.</param>
    public void Activate(GameObject prefab)
    {
        int id = prefab.GetInstanceID();
        if (!this.ContainsKey(id))
        {
            GameObject newInstance = Object.Instantiate(prefab, context);
            newInstance.SetActive(false);
            this.Add(id, newInstance);
        }
        CurrentKey = id;
    }
}
