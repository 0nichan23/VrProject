using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageHandler 
{
    [SerializeField] private float amount;
    private List<float> modifiers = new List<float>();
    public float Amount { get => amount; }

    public void OverrideAmount(float givenAmount)
    {
        amount = givenAmount;
    }
    public void AddMod(float givenMod)
    {
        modifiers.Add(givenMod);
    }

    public void ClearMods()
    {
        modifiers.Clear();
    }

    public float GetFinalDamage()
    {
        float baseAmount = amount;
        foreach (var item in modifiers)
        {
            baseAmount *= item;
        }
        return baseAmount;
    }

}
