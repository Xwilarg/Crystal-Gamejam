using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Artillery : MonoBehaviour
{
    public static Artillery S;

    [SerializeField]
    private Sprite[] _steps;

    [SerializeField]
    private GameObject _shell;

    [SerializeField]
    private GameObject _dropShellObj, _dropShellParent;

    [SerializeField]
    private FadeOut _bloodCover;

    [SerializeField]
    private TMP_Text _gameOver;

    private int _indexDamage;

    private int _index;

    private SpriteRenderer _sr;

    private AudioSource _audio;

    private void Awake()
    {
        S = this;
        _sr = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
    }

    public void AddStep()
    {
        if (_index < _steps.Length)
        {
            _sr.sprite = _steps[_index++];
        }
        else
        {
            Instantiate(_shell, transform.position, Quaternion.identity);
        }
    }

    public void DropShell()
    {
        var go = Instantiate(_dropShellObj, _dropShellParent.transform);
        go.transform.position += Vector3.up * 1000f;
    }

    public void TakeDamage()
    {
        _indexDamage++;
        if (_indexDamage <= 3)
        {
            var color = _indexDamage / 3f;
            _bloodCover.TakeFade(color);
            _audio.Play();
            if (_indexDamage == 3)
            {
                _gameOver.gameObject.SetActive(true);
            }
        }
    }

    public bool IsAlive => _indexDamage < 3;
}
