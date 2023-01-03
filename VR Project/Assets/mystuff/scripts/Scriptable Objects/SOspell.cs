using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "New Spell", menuName = "Create Spell", order = 51)]
public class SOspell : ScriptableObject
{
    public SpellType Type;
    public string Gesture;
    public float Accuracy;
    public float Cooldown;

    public enum SpellType : int
    {
        Fireball,
        Lightning,
        IceSpike,
        Shield
    }
}
