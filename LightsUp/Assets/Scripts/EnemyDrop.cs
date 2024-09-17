using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject itemDrop;
    public Transform dropPoint;

    void OnDestroy()
    {
        dropItem();
    }

    void dropItem()
    {
        if (itemDrop != null && dropPoint != null)
        {
            Instantiate(itemDrop, dropPoint.position, Quaternion.identity);
        }
    }
}
