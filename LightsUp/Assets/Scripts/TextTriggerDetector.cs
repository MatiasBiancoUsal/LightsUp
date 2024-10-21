using UnityEngine;
using TMPro; 

public class TextTriggerDetector : MonoBehaviour
{
    public TextMeshProUGUI uiText; 
    public float fadeSpeed = 1f; 

    private bool playerInZone = false;
    private Color originalColor;

    void Start()
    {
        
        if (uiText == null)
        {
            Debug.LogError("uiText no está asignado en el Inspector.");
            return;
        }

        
        originalColor = uiText.color;
        originalColor.a = 0f; 
        uiText.color = originalColor; 
    }

    void Update()
    {
        if (playerInZone)
        {
            
            originalColor.a = Mathf.Lerp(originalColor.a, 1f, Time.deltaTime * fadeSpeed);
            uiText.color = originalColor;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            originalColor.a = 0f; 
            uiText.color = originalColor;
        }
    }
}
