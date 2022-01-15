using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _xBound, _yBound;

    [SerializeField]
    private int _count;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            Instantiate(_prefab, new Vector2(Random.Range(-_xBound, _xBound), Random.Range(-_yBound, _yBound)), Quaternion.identity);
        }
    }
}
