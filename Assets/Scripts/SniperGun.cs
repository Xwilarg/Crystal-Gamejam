using UnityEngine;

public class SniperGun : MonoBehaviour
{
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
            _canShoot = 1f;
            _audio.Play();
        }
    }
}
