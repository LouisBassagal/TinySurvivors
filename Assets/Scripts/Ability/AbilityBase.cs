using System;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public AbilitySO Data { get; private set; }
    public int Level { get; private set; } = 1;

    public void Initialize(AbilitySO data)
    {
        Data = data;
        Level = 1;
    }

    public void LevelUp()
    {
        if (Level < Data.MaxLevel)
        {
            Level++;
            OnLevelUp();
        }
    }
    protected abstract void OnLevelUp();
}