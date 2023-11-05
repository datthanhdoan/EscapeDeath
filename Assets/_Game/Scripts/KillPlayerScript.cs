using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class KillPlayerScript : MonoBehaviour
{
    private PlayerMoverment playerMoverment;


    protected virtual void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool x = true;
            playerMoverment.setDead(x);
        }
    }
}
