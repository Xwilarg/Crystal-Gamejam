using UnityEngine;

public class Artillery : MonoBehaviour
{
    public static Artillery S;

    [SerializeField]
    private Sprite[] _steps;

    [SerializeField]
    private GameObject _shell;

    private int _index;

    private SpriteRenderer _sr;

    private void Awake()
    {
        S = this;
        _sr = GetComponent<SpriteRenderer>();
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
}
