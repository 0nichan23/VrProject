using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] RotationComponents;

    public void SwitchRotationType(int i)
    {
        foreach (MonoBehaviour component in RotationComponents)
        {
            component.enabled = false;
        }
        RotationComponents[i].enabled = true;
    }
}
