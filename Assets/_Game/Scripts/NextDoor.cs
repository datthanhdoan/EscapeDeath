using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NextDoor : MonoBehaviour
{

    private bool isKey = true;
    private Animator anim;
    private PlayerMoverment playerMoverment;
    private GameManagerScript gameManager;
    private AudioManagerScript audioManagerScript;
    private float timer = 0f;
    private bool doorOpen = false;
    public void setKey(bool isKey)
    {
        this.isKey = isKey;
    }
    public bool getKey()
    {
        return isKey;
    }
    public bool getDoor()
    {
        return doorOpen;
    }
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

    void Update()
    {
        if (doorOpen)
        {
            timer += Time.deltaTime;
            if (timer >= 2.2f)
            {
                gameManager.nextScreen();
                doorOpen = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isKey && !playerMoverment.getDead())
        {
            audioManagerScript.SoundEffect(audioManagerScript.DoorOpen);
            anim.CrossFade(Open, 0, 0);
            doorOpen = true;
        }
    }
    private static readonly int Open = Animator.StringToHash("DoorOpen");

}
