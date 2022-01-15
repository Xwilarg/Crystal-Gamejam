using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    private float _baseAlpha;
    private Image _sprite;
    private float _prog;

    private void Start()
    {
        _sprite = GetComponent<Image>();
    }

    public void TakeFade(float alpha)
    {
        _baseAlpha = alpha;
        _prog = 0f;
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1f);
    }

    private void Update()
    {
        if (_sprite.color.a > _baseAlpha)
        {
            _prog += Time.deltaTime;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, Mathf.Lerp(1f, _baseAlpha, _prog));
        }
    }
}
