using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _xBound, _yBound;

    [SerializeField]
    private int _count;

    [SerializeField]
    private bool _enabled, _repeat, _spe;

    [SerializeField]
    private float _repeatRate;

    private float _timer;

    public void EnableSpawn()
    {
        _enabled = true;
    }

    private void Update()
    {
        if (!_enabled || !Artillery.S.IsAlive)
        {
            return;
        }
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            for (int i = 0; i < _count; i++)
            {
                var go = Instantiate(_prefab, (Vector2)transform.position + new Vector2(Random.Range(-_xBound, _xBound), Random.Range(-_yBound, _yBound)), Quaternion.identity);
                if (_spe)
                {
                    go.GetComponent<ChickenBehavior>().EnableSpe();
                }
            }
            if (!_repeat)
            {
                _enabled = false;
            }
            else
            {
                _timer = _repeatRate;
                if (_repeatRate > 1f)
                {
                    _repeatRate -= .1f;
                }
            }
        }
    }
}
