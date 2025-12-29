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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            Debug.Log("+15 HP");
        }
        else if (other.CompareTag("Memory"))
        {
            Destroy(other.gameObject);
            Debug.Log("Memory Fragments Collected");
        }
    }
}
