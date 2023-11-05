using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField] private GameObject rock;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!rock.activeSelf)
        {
            rock.SetActive(true);
            anim.CrossFade(On, 0, 0);
        }
    }
    private static readonly int On = Animator.StringToHash("switch_on");
}
