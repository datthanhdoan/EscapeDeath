using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public Transform PositionSpawn;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnArrow()
    {
        Instantiate(Arrow, PositionSpawn.position, Quaternion.identity);
    }
}
