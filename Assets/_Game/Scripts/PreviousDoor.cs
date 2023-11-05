using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviousDoor : MonoBehaviour
{

    private Animator anim;
    private GameManagerScript gameManager;
    private float timer = 0f;
    private bool doorOpen = false;
    private AudioManagerScript audioManagerScript;
    private PlayerMoverment playerMoverment;
    private void Awake()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManagerScript>();
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }
    public bool getDoor()
    {
        return doorOpen;
    }

    void Update()
    {
        if (doorOpen)
        {
            timer += Time.deltaTime;
            if (timer >= 2.2f)
            {
                gameManager.previousScreen();
                doorOpen = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerMoverment.getDead())
        {
            audioManagerScript.SoundEffect(audioManagerScript.DoorOpen);
            anim.CrossFade(Open, 0, 0);
            doorOpen = true;
        }
    }
    private static readonly int Open = Animator.StringToHash("DoorOpen");

}
