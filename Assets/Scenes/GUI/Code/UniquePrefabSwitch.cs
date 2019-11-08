using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniquePrefabSwitch
{
    /// <summary>
    /// Context under which new objects should be stored.
    /// </summary>
    private readonly Transform context;

    /// <summary>
    /// Dictionary of all previously seen prefabs.
    /// </summary>
    /// This relies on the user being honest - prefabs are essentially GameObjects that exist in
    /// the filesystem. But it should not break in either case.
    private Dictionary<int, GameObject> prefabs = new Dictionary<int, GameObject>();

    /// <summary>
    /// Active prefab.
    /// </summary>
    private GameObject current;

    public UniquePrefabSwitch(Transform context)
    {
        this.context = context;
    }

    private void Change(int id)
    {
        current?.SetActive(false);

        GameObject next = prefabs[id];
        next.SetActive(true);
        current = next;
    }

    public void Activate(GameObject prefab)
    {
        int Id = prefab.GetInstanceID();

        if (!prefabs.ContainsKey(Id))
        {
            prefabs.Add(Id, Object.Instantiate(prefab, context));
        }

        Change(Id);
    }
}
