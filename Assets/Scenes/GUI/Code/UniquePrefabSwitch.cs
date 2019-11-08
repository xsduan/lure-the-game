using UnityEngine;

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
