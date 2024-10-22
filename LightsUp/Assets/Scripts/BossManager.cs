using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool started;
    public static BossManager instance;

    public enum Boss
    {
        Envidia,
        Codicia,
        Gula,
        Diablo
    }

    public Boss BossName;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damage)
    {
        if (BossName == Boss.Gula)
        {
            BossHealth.Instance.ReceiveDamageGula(damage);
        }
        if (BossName == Boss.Codicia)
        {
            BossHealth.Instance.ReceiveDamageCodicia(damage);
        }
    }
}
