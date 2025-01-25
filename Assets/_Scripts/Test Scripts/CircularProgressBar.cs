using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    [SerializeField] private Image _foreground;
    [SerializeField] private float _targetFade;
    [SerializeField] private float _breathDuration;
    [SerializeField] private GameObject _brainGood;
    [SerializeField] private GameObject _brainBad;
    private void Start()
    {
        _foreground.DOFade(_targetFade, _breathDuration).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        GameManager.Instance.ChangedToHell += OnHell;
    }
    public void UpdateBar(float value, float min, float max)
    {
        float t = Mathf.InverseLerp(min, max, value);

        float scaleChange = Mathf.Lerp(0.45f, 1, t);
        Vector3 scale = _foreground.transform.localScale;
        scale.x = scaleChange;
        scale.y = scaleChange;
        _foreground.transform.localScale = scale;
    }

    public void OnHell()
    {
        _foreground.enabled = false;
        _brainGood.gameObject.SetActive(false);
        _brainBad.gameObject.SetActive(true);
    }

}
