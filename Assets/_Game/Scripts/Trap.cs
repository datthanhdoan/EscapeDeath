using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private PlayerMoverment playerMoverment;
    public GameObject _bloodEffect;
    [SerializeField] private int _damageAttack = 10;
    [SerializeField] protected internal float _delayAttack = 2f;
    protected internal float _timerDelayAttack = 0f;
    private Animator _anim;
    public string _animAttackName;
    private static int Attack;
    private bool PlayerIn = false;
    // Start is called before the first frame update
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }
    protected virtual void Start()
    {
        if (!string.IsNullOrEmpty(_animAttackName))
        {
            Attack = Animator.StringToHash(_animAttackName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerDelayAttack <= 0)
        {
            if (PlayerIn)
            {
                if (!string.IsNullOrEmpty(_animAttackName))
                {
                    _anim.CrossFade(Attack, 0, 0);
                }
                playerMoverment.TakeDamage(_damageAttack);
                _timerDelayAttack = _delayAttack;
                if (_bloodEffect != null)
                {
                    Instantiate(_bloodEffect, transform.position, Quaternion.identity);
                }
            }

        }
        else
        {
            _timerDelayAttack -= Time.deltaTime;

        }

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerIn = false;
        }
    }
}


