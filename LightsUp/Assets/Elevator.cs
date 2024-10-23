using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator animator; 
    private bool jugadorEnAscensor = false;
    private bool palancaActivada = false;

    private void Update()
    {
        
        if (jugadorEnAscensor && palancaActivada)
        {
            ActivarAnimacion();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            jugadorEnAscensor = true; 
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            jugadorEnAscensor = false; 
        }
    }

    public void ActivarAnimacion()
    {
        if (jugadorEnAscensor)
        {
            animator.SetTrigger("isUp"); 
            palancaActivada = false; 
        }
    }

    public void ActivarPalanca()
    {
        palancaActivada = true; 
    }
}
