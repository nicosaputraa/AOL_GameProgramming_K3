using UnityEngine;

public class Player : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heart"))
        {
            Debug.Log("+15 Hp");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Memory"))
        {
            Debug.Log("Memory Fragments Collected");
            Destroy(other.gameObject);
        }
    }
}
