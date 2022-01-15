using UnityEngine;

public class ChickenBehavior : MonoBehaviour
{
    private float _dirTimer;
    private Vector2 _dir;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _speed = 20f;

    private Vector2 _initPos;
    private float _maxDistFromInitPos = 1f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _initPos = transform.position;
    }

    private void Update()
    {
        _dirTimer -= Time.deltaTime;
        if (_dirTimer <= 0f)
        {
            _dirTimer = Random.Range(1f, 3f);

            if (Vector2.Distance(transform.position, _initPos) > _maxDistFromInitPos) // Chicken going too far away
            {
                _dir = _initPos - (Vector2)transform.position;
            }
            else
            {
                _dir = Random.insideUnitCircle;
            }
            _dir.Normalize();
        }

        _rb.velocity = _dir * Time.deltaTime * _speed;
        _sr.flipX = _rb.velocity.x < 0f;
    }
}
