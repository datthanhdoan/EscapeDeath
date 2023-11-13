using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_enemy : MonoBehaviour
{
    public GameObject enemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        enemy.SetActive(true);
    }

}
