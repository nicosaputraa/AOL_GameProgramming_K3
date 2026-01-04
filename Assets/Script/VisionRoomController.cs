using UnityEngine;

public class VisionRoomController : MonoBehaviour
{
    [Header("Vision Mask")]
    public Transform visionMask;

    [Header("Vision Size")]
    public float normalSize = 6f;
    public float largeRoomSize = 12f;

    [Header("Transition")]
    public float smoothSpeed = 5f;

    float targetSize;

    void Start()
    {
        if (visionMask == null)
        {
            Debug.LogError("VisionMask belum di-assign!");
            return;
        }

        targetSize = normalSize;
        visionMask.localScale = Vector3.one * normalSize;
    }

    void Update()
    {
        float currentSize = visionMask.localScale.x;
        float newSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * smoothSpeed);
        visionMask.localScale = Vector3.one * newSize;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Masuk ruangan besar
        if (other.CompareTag("LargeRoom"))
        {
            targetSize = largeRoomSize;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Keluar ruangan besar
        if (other.CompareTag("LargeRoom"))
        {
            targetSize = normalSize;
        }
    }
}
