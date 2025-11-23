using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform target;
    Vector3 distance;

    void Start()
    {
        distance = transform.position - target.position;
    }
    void LateUpdate() // Update yerine LateUpdate daha pürüzsüz takip sağlar
    {
        if (target != null) // Hedef yok olmadıysa (yem olup silinmediyse)
        {
            // Topun yeni pozisyonuna hafızadaki mesafeyi ekle
            transform.position = target.position + distance;
        }
    }
}
