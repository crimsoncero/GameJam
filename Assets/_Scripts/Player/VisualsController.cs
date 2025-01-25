using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class VisualsController : MonoBehaviour
{

    [SerializeField] private Material _goodMat;
    [SerializeField] private Material _badMat;
   
    [SerializeField] private float _morphDuration;

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
    }

    private void ResetMats()
    {
        _goodMat.SetFloat("_Fade", 1);
        _badMat.SetFloat("_Fade", 1);
    }
    private void OnApplicationQuit()
    {
        _goodMat.SetFloat("_Fade", 1);
        _badMat.SetFloat("_Fade", 1);
    }
}
