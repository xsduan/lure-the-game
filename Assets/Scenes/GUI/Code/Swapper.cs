using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A utility class to help manage mutually exclusive items.
/// </summary>
/// <typeparam name="TKey">Key to identify values by.</typeparam>
/// <typeparam name="TValue">Object that can be toggled on and off.</typeparam>
public class Swapper<TKey, TValue> : Dictionary<TKey, TValue>
{
    private TKey currentKey;
    private readonly Action<TValue, bool> defaultCallback;

    /// <summary>
    /// The key that is currently active, i.e. selected.
    /// </summary>
    public TKey CurrentKey { get => currentKey; set => Swap(value); }

    public Swapper(Action<TValue, bool> defaultCallback) : base()
    {
        this.defaultCallback = defaultCallback;
    }

    public Swapper(IDictionary<TKey, TValue> other, Action<TValue, bool> defaultCallback) : base(other)
    {
        this.defaultCallback = defaultCallback;
    }
    
    /// <summary>
    /// Swap to new key.
    /// </summary>
    /// <param name="newKey">New key to switch to.</param>
    /// <param name="overrideCallback">Optional override action, in case unique behavior is needed.</param>
    public void Swap(TKey newKey, Action<TValue, bool> overrideCallback = null)
    {
        var callback = overrideCallback ?? defaultCallback;

        this.TryGetValue(newKey, out TValue newValue);
        if (newValue == null)
        {
            Debug.LogWarning($"There isn't a value for {newKey}. Ignoring.");
            return;
        }

        this.TryGetValue(currentKey, out TValue currentValue);

        callback(currentValue, false);
        callback(newValue, true);

        currentKey = newKey;
    }
}
