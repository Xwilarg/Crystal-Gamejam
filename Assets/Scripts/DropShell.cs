using UnityEngine;

public class DropShell : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 1200f);
        if (transform.position.y < 0f)
        {
            Destroy(gameObject);
            Artillery.S.TakeDamage();
        }
    }
}
