using UnityEngine;

public class SniperGun : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        Cursor.visible = false;
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
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
        }
    }
}
