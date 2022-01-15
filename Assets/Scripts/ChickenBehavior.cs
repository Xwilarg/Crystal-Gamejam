using UnityEngine;

public class ChickenBehavior : MonoBehaviour
{
    [SerializeField]
    private Sprite _deadSprite;

    private float _dirTimer;
    private Vector2 _dir;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _minSpeed = 15f, _maxSpeed = 20f;
    private float _speed = 20f;

    private Vector2 _initPos;
    private float _maxDistFromInitPos = 1f;

    private bool _isDead;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _initPos = transform.position;
    }

    private void Update()
    {
        if (_isDead) // Dead chickens don't move
        {
            return;
        }
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

        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rb.velocity = _dir * Time.deltaTime * _speed;
        _sr.flipX = _rb.velocity.x < 0f;

        _sr.sortingOrder = -(int)(transform.position.y * 1000f);

        Debug.DrawLine(_initPos, transform.position, Color.red);
    }

    public void Die()
    {
        _isDead = true;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
        _sr.sprite = _deadSprite;
    }
}
