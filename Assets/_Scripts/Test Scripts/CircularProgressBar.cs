using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    [SerializeField] private Image _foreground;

    public void UpdateBar(float value, float min, float max)
    {
        float t = Mathf.InverseLerp(min, max, value);

        float scaleChange = Mathf.Lerp(0, 1, t);
        Vector3 scale = _foreground.transform.localScale;
        scale.x = scaleChange;
        scale.y = scaleChange;
        _foreground.transform.localScale = scale;
    }

}
