using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Door door; 
    private bool _isPlayerOrBoxOnButton = false;
    private Coroutine _closeCoroutine; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            _isPlayerOrBoxOnButton = true;
            door.SetIsOpening(true); 

            
            if (_closeCoroutine != null)
            {
                StopCoroutine(_closeCoroutine);
                _closeCoroutine = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            _isPlayerOrBoxOnButton = false;

           
            if (_closeCoroutine == null)
            {
                _closeCoroutine = StartCoroutine(CloseDoorAfterDelay(2f));
            }
        }
    }

    private IEnumerator CloseDoorAfterDelay(float delay)
    {
       
        yield return new WaitForSeconds(delay);

        
        if (!_isPlayerOrBoxOnButton)
        {
            door.SetIsOpening(false); 
        }

        _closeCoroutine = null; 
    }
}