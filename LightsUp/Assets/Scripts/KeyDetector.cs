using UnityEngine;

public class KeyDetector : MonoBehaviour
{
    public DoorKey door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (door != null)
            {
                door.SetIsOpening(true);
            }

            
            Destroy(gameObject);
        }
    }
}
