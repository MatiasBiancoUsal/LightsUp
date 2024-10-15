using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    public float health = 10f;

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
