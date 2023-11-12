using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float _timeDestroy = 5f;
    void Start()
    {
        Destroy(gameObject, _timeDestroy);
    }
}
