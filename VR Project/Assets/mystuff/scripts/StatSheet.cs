using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatSheet : MonoBehaviour
{
    private float _cooldown;
    [HideInInspector] public bool CanAttack = true; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldown > 0) { _cooldown-=Time.deltaTime; }
        else if (_cooldown<0) { _cooldown = 0; CanAttack = true; }
    }

    public void UseAttack(float cooldown)
    {
        if (CanAttack)
        {
            _cooldown = cooldown;
            CanAttack = false;
        }
    }
}
