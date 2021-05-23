using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    public float coinNum = 0;
    public float moveSpeed = 2;
    public float maxSpeed;
    public float jumpForce = 10;
    public Transform groundCheck;

    private bool grounded = true;
    private bool enemies = false;

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
        maxSpeed = moveSpeed * (coinNum+1);
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        if (estaVivo)
        {
            rb.AddForce(Vector2.right * moveSpeed * (coinNum + 1));

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
        }
    }
}


