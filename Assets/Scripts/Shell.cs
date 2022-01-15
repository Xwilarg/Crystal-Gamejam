using UnityEngine;

public class Shell : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 30f);
    }
}
