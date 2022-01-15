using UnityEngine;

public class ChickenBehavior : MonoBehaviour
{
    private float _dirTimer;
    private Vector2 _dir;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _speed = 20f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _dirTimer -= Time.deltaTime;
        if (_dirTimer <= 0f)
        {
            _dir = Random.insideUnitCircle.normalized;
            _dirTimer = Random.Range(1f, 3f);
        }

        _rb.velocity = _dir * Time.deltaTime * _speed;
        _sr.flipX = _rb.velocity.x < 0f;
    }
}
