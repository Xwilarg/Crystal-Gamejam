using UnityEngine;

public class ChickenBehavior : MonoBehaviour
{
    [SerializeField]
    private Sprite _deadSprite;

    [SerializeField]
    private GameObject _box;

    private float _dirTimer;
    private Vector2 _dir;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _minSpeed = 15f, _maxSpeed = 20f;
    private float _speed = 20f;

    private Vector2 _initPos;
    private float _maxDistFromInitPos = 1f;

    private ParticleSystem _particles;

    private bool _isDead;
    private bool _spe;

    public bool IsDead => _isDead;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _particles = GetComponentInChildren<ParticleSystem>();
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

            if (_spe)
            {
                _dir = -_initPos;
            }
            else if (Vector2.Distance(transform.position, _initPos) > _maxDistFromInitPos) // Chicken going too far away
            {
                _dir = _initPos - (Vector2)transform.position;
                _speed = Random.Range(_minSpeed, _maxSpeed);
            }
            else
            {
                _dir = Random.insideUnitCircle;
                _speed = Random.Range(_minSpeed, _maxSpeed);
            }
            _dir.Normalize();
        }

        _rb.velocity = _dir * Time.deltaTime * _speed;
        transform.localScale = new Vector3(_rb.velocity.x < 0f ? -1f : 1f, 1f, 1f);

        _sr.sortingOrder = -(int)(transform.position.y * 1000f);

        if (_spe && Vector2.Distance(Vector2.zero, transform.position) < .1f)
        {
            Artillery.S.AddStep();
            DisableSpe();
        }

        Debug.DrawLine(_initPos, transform.position, Color.red);
    }

    public void EnableSpe()
    {
        _spe = true;
        _box.SetActive(true);
        gameObject.layer = 7;
        _speed = 40f;
    }

    public void DisableSpe()
    {
        _spe = false;
        _box.SetActive(false);
        gameObject.layer = 6;
    }

    public void Die()
    {
        _isDead = true;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
        _sr.sprite = _deadSprite;
        _box.SetActive(false);
        //_particles.Play();
    }
}
