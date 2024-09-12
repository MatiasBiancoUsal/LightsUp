using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public PlayerMov player;
    public BoxCollider2D playerCollider;

    [SerializeField] Vector2 standOffset, standSize;
    [SerializeField] Vector2 crouchOffset, crouchSize;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMov>();

        standSize = playerCollider.size;
        standOffset = playerCollider.offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isCrouched)
        {
            playerCollider.size = crouchSize;
            playerCollider.offset = crouchOffset;
        }

        if (!player.isCrouched)
        {
            playerCollider.size = standSize;
            playerCollider.offset = standOffset;
        }
    }
}
