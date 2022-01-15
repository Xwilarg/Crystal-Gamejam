using UnityEngine;
using UnityEngine.UI;

public class SniperGun : MonoBehaviour
{
    [SerializeField]
    private AudioClip _shoot, _bip;

    [SerializeField]
    private Image[] _ammoSprites;
    private int _indexAmmo;

    [SerializeField]
    private Transform _parentKillCount;

    [SerializeField]
    private GameObject _killCountPrefab;
    private GameObject _current;

    [SerializeField]
    private Sprite[] _killCount;
    private int _killCountIndex = -1, _killCountOffset;

    private Camera _cam;
    private float _canShoot;

    private AudioSource _audio;

    bool _toldReady = true;

    private void Start()
    {
        Cursor.visible = false;
        _cam = Camera.main;
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _canShoot -= Time.deltaTime;
        if (_canShoot < 0f)
        {
            if (!_toldReady)
            {
                _audio.clip = _bip;
                _audio.Play();
                _toldReady = true;
            }
            if (_indexAmmo == 0 && !_ammoSprites[0].gameObject.activeInHierarchy) // Reload done
            {
                foreach (var sp in _ammoSprites)
                {
                    sp.gameObject.SetActive(true);
                }
            }
        }
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && _canShoot < 0f)
        {
            Vector3 worldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
            if (hit)
            {
                Debug.Log($"Hit {hit.collider.name}");
                var chicken = hit.collider.GetComponent<ChickenBehavior>();
                if (chicken != null && !chicken.IsDead)
                {
                    chicken.Die();
                    if (_killCountIndex == -1)
                    {
                        _current = Instantiate(_killCountPrefab, _parentKillCount);
                        _current.transform.position = new Vector2(_current.transform.position.x + 64f * _killCountOffset, _current.transform.position.y);
                        _killCountIndex++;
                    }
                    _current.GetComponent<Image>().sprite = _killCount[_killCountIndex];
                    _killCountIndex++;
                    if (_killCountIndex == _killCount.Length)
                    {
                        _killCountIndex = -1;
                        _killCountOffset += 1;
                    }
                }
            }
            _audio.clip = _shoot;
            _audio.Play();
            _toldReady = false;
            _ammoSprites[_indexAmmo].gameObject.SetActive(false);
            _indexAmmo++;
            if (_indexAmmo == _ammoSprites.Length)
            {
                _indexAmmo = 0;
                _canShoot = 3f;
            }
            else
            {
                _canShoot = 1f;
            }
        }
    }
}
