using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitAndDrop());
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 30f);
    }

    private IEnumerator WaitAndDrop()
    {
        yield return new WaitForSeconds(3f);
        Artillery.S.DropShell();
        Destroy(gameObject);
    }
}
