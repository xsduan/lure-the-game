using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper<TKey, TValue> : Dictionary<TKey, TValue>
{
    private TKey currentKey;
    private readonly Action<TValue, bool> defaultCallback;

    public TKey CurrentKey { get => currentKey; set => ChoiceSetSwap(value); }

    public Swapper(Action<TValue, bool> defaultCallback) : base()
    {
        this.defaultCallback = defaultCallback;
    }

    public Swapper(IDictionary<TKey, TValue> other, Action<TValue, bool> defaultCallback) : base(other)
    {
        this.defaultCallback = defaultCallback;
    }
    
    public void ChoiceSetSwap(TKey newKey, Action<TValue, bool> overrideCallback = null)
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
