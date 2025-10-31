using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float timeout = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeout);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
