using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator animator;
    private bool esEmpujada = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !esEmpujada)
        {
            esEmpujada = true;
            animator.SetTrigger("isPushing");

       
            Elevator ascensor = FindObjectOfType<Elevator>();
            if (ascensor != null)
            {
                ascensor.ActivarPalanca();
            }
        }
    }

    public void ResetearPalanca()
    {
        esEmpujada = false;
        animator.SetTrigger("resetear");
    }
}
