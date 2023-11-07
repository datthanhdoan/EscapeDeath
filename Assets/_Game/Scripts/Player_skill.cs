using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_skill : MonoBehaviour
{
    
    // public AnimatedSprite Skill_1;
    public SpriteRenderer spriteRenderer { get; private set; }
    public new Collider2D collider { get; private set; }
    public AnimationFrame skill_1;

    public float speed = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ActivateSkill1();
        }
    }

    
   

    public void Skill_1() {
        enabled = true;
        spriteRenderer.enabled = true;
        skill_1.GetComponent<Collider2D>().enabled = true;
        skill_1.enabled = true;
        skill_1.spriteRenderer.enabled = true;
    }
    
    public IEnumerator ActivateSkill_1() {
        Skill_1(); // Gọi hàm Skill_1()
    
        yield return new WaitForSeconds(speed); // Đợi 1 giây

        // Tắt các thành phần đã được kích hoạt trong hàm Skill_1()
        spriteRenderer.enabled = true;
        skill_1.GetComponent<Collider2D>().enabled = false;
        skill_1.enabled = false;
        skill_1.spriteRenderer.enabled = false;
    }
    public void ActivateSkill1()
    {
        StartCoroutine(ActivateSkill_1());     
    }
}
