using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public static PlayerControl playerControl;
    public float MaxSpeedMultply = 0.5F;
    private float speedBase = 10;
    public float moveSpeed = 7;
    public float maxSpeed = 100;
    public float jumpForce = 500;
    public Transform groundCheck;

    private bool grounded = true;

    private Rigidbody2D rb;
    private bool estaVivo = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        if (estaVivo)
        { 
            moveSpeed = speedBase * (LevelManager.levelManager.keysAtual + 1) * MaxSpeedMultply;
            
            
            rb.AddForce(Vector2.right * moveSpeed);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x)* maxSpeed, rb.velocity.y);
            }
            if (grounded)
            {
                if (Input.GetKey("space"))
                {
                    rb.AddForce(new Vector2(0, jumpForce));
                    grounded = false;
                }
            }
           
        }else
        {
            Morreu();
        }
        
    }
    public void Morreu()
    {
        if (estaVivo)
        {
            estaVivo = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("GameOver", 2f);
        }
    }

    public void GameOver()
    {
        LevelManager.levelManager.GameOver();
    }
}


