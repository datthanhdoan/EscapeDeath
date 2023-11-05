using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    private float distanceX, distance;
    private PlayerMoverment playerMoverment;
    private AudioManagerScript audioManagerScript;
    private DogMoverment dogmov;
    private int currentState;
    private void Awake()
    {
        dogmov = FindObjectOfType<DogMoverment>();
        distanceX = dogmov.getDistanceX();
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();

    }
    private void Start()
    {

        playerMoverment = player.GetComponent<PlayerMoverment>();

    }
    private int GetState()
    {

        if (playerMoverment.getDead()) return Idle;
        if (dogmov.getDead()) return Dead;
        return dogmov.getIsRunning() ? Run : Idle;

    }
    // Update is called once per frame
    void Update()
    {
        var state = GetState();
        if (state != currentState)
        {
            anim.CrossFade(state, 0, 0);
            currentState = state;
            if (state == Run)
            {
                audioManagerScript.SoundEffect(audioManagerScript.dogWol);
            }
            if (state == Dead)
            {
                audioManagerScript.SoundEffect(audioManagerScript.dogDead);
            }

        }

    }
    #region Animation Keys
    private static readonly int Dead = Animator.StringToHash("dog_die");
    private static readonly int Run = Animator.StringToHash("dog_run");
    private static readonly int Idle = Animator.StringToHash("dog_idle");
    #endregion
}
