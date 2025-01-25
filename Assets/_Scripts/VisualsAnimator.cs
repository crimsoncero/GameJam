using System.Collections.Generic;
using UnityEngine;

public class VisualsAnimator : MonoBehaviour
{
    [field: SerializeField] public List<Animator> AnimList { get; private set; }

    public void SetBool(string varName, bool value)
    {
        foreach(var anim in AnimList)
        {
            anim.SetBool(varName, value);
        }
    }

    public void SetFloat(string varName, float value)
    {
        foreach (var anim in AnimList)
        {
            anim.SetFloat(varName, value);
        }
    }
}
