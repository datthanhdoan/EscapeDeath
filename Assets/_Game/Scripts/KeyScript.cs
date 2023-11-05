using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class KeyScript : MonoBehaviour
{
    private NextDoor nextDoor;
    [SerializeField] private ParticleSystem parac;
    private Animator anim;
    private bool waiting = false;
    private float timer = 0;
    [SerializeField] private Light2D light;
    private float initialIntensity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        nextDoor = FindObjectOfType<NextDoor>();
        initialIntensity = light.intensity;
    }

    private void Update()
    {
        if (waiting)
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f)
            {
                Destroy(gameObject);
                waiting = false;
            }
            else
            {

                light.intensity = Mathf.Lerp(initialIntensity, 0f, timer / 1.25f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nextDoor.setKey(true);
            anim.CrossFade("KeyZoomOut", 0, 0);
            parac.Play();
            waiting = true;
        }
    }
}
