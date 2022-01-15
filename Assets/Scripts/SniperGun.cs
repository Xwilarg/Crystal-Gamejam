using UnityEngine;
using UnityEngine.UI;

public class SniperGun : MonoBehaviour
{
    [SerializeField]
    private Image[] _ammoSprites;
    private int _indexAmmo;

    private Camera _cam;
    private float _canShoot;

    private AudioSource _audio;

    private void Start()
    {
        Cursor.visible = false;
        _cam = Camera.main;
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _canShoot -= Time.deltaTime;
        if (_canShoot < 0f && _indexAmmo == 0 && !_ammoSprites[0].gameObject.activeInHierarchy) // Reload done
        {
            foreach (var sp in _ammoSprites)
            {
                sp.gameObject.SetActive(true);
            }
        }
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && _canShoot < 0f)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
            if (hit)
            {
                Debug.Log($"Hit {hit.collider.name}");
                var chicken = hit.collider.GetComponent<ChickenBehavior>();
                if (chicken != null)
                {
                    chicken.Die();
                }
            }
            _audio.Play();
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