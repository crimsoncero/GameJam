using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine;

public class VisualsController : MonoBehaviour
{

    [SerializeField] private Material _goodMat;
    [SerializeField] private Material _badMat;
   
    [SerializeField] private float _morphDuration;

    [SerializeField] private List<ParticleSystem> _smokeList;

    [MMFInspectorButton("Morph")]
    [SerializeField] private bool morphtest;
    
    [MMFInspectorButton("ResetMats")]
    [SerializeField] private bool resetTest;

    private void Start()
    {
        _goodMat.SetFloat("_Fade", 1);
        _badMat.SetFloat("_Fade", 1);
    }

    public void Morph()
    {
        _goodMat.DOFloat(0, "_Fade", _morphDuration);
        _badMat.DOFloat(0, "_Fade", _morphDuration);
        foreach(ParticleSystem p in _smokeList)
        {
            p.gameObject.SetActive(true);
        }
        TestUI.Instance._bubbleProgressBar.OnHell();
    }

    private void ResetMats()
    {
        _goodMat.SetFloat("_Fade", 1);
        _badMat.SetFloat("_Fade", 1);
        foreach (ParticleSystem p in _smokeList)
        {
            p.gameObject.SetActive(false);
        }
    }
    private void OnApplicationQuit()
    {
        _goodMat.SetFloat("_Fade", 1);
        _badMat.SetFloat("_Fade", 1);
    }
}
