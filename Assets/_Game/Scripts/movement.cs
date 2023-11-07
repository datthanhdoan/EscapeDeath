using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private bool isSkillActive = false; 
    bool isGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkillActive == false) {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(x*speed, y*speed);
            transform.Translate(move*Time.deltaTime);    

            if (Input.GetKey(KeyCode.Space) && isGround == true) {
                rb.AddForce(Vector2.up *400);
                isGround = false;
            }
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGround = true;
        }
    }
   void CheckSkillTag()
    {
        GameObject skillObject = GameObject.FindGameObjectWithTag("skill");

        if (skillObject != null)
        {
            isSkillActive = skillObject.activeSelf;
        }
    }
}
