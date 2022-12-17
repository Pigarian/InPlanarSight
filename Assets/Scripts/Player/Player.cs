using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float jumpHeldMax = 0.2f;
    [SerializeField] private Sprite[] eyes;
    [SerializeField] private Light2D mapLight;
    [SerializeField] private int betweenBlink = 2000;
    [SerializeField] private float blinkTime;
    [SerializeField] private bool autoBlink = false;
    public bool canInput = false;
    
    private System.Random rng;
    private Rigidbody2D rb;
    private float xMoving;
    private bool spacePress;
    public bool canJump = true;
    public bool subLevelJump = true;
    private SpriteRenderer look;
    private int counter;

    private bool left;
    public bool closeLeft;
    public bool closeRight;

    // Start is called before the first frame update
    void Start()
    {
        rng = new System.Random(Mathf.CeilToInt(Time.time));
        rb = GetComponent<Rigidbody2D>();
        look = GetComponent<SpriteRenderer>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (autoBlink)
        {
            counter++;
            if (counter >= betweenBlink)
            {
                StartCoroutine(Blink());
                counter = rng.Next(0,betweenBlink);
            }
        }
        
        if (!canInput) return;
        xMoving = Input.GetAxisRaw("Horizontal");
        if (xMoving < 0) left = true;
        else if (xMoving > 0) left = false;
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            spacePress = true;
            canJump = false;
        }

        closeLeft = Input.GetMouseButton(0);
        closeRight = Input.GetMouseButton(1);
    }

    private void FixedUpdate()
    {
        if (autoBlink) return;
        
        rb.velocity = new Vector2(xMoving * speed, rb.velocity.y);
        if (spacePress && subLevelJump)
        {
            spacePress = false;
            StartCoroutine(Jump(Vector2.up));
        }
        
        look.flipX = left;
        switch (closeLeft)
        {
            case false when !closeRight:
                look.sprite = eyes[0];
                mapLight.intensity = 1;
                break;
            case true when closeRight:
                look.sprite = eyes[3];
                mapLight.intensity = 0;
                break;
            case true when !closeRight && !left:
                look.sprite = eyes[1];
                mapLight.intensity = .6f;
                break;
            case true when !closeRight && left:
                look.sprite = eyes[2];
                mapLight.intensity = .6f;
                break;
            case false when closeRight && left:
                look.sprite = eyes[1];
                mapLight.intensity = .3f;
                break;
            case false when closeRight && !left:
                look.sprite = eyes[2];
                mapLight.intensity = .3f;
                break;
        }
    }

    IEnumerator Jump(Vector2 dir)
    {
        for (float jumpTimer = jumpHeldMax; jumpTimer >= 0 && Input.GetKey(KeyCode.Space); jumpTimer -= Time.fixedDeltaTime)
        {
            rb.velocity = new Vector2(2 * rb.velocity.x + dir.x, dir.y * jumpForce);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("jumpable") || collision.gameObject.CompareTag("cube")) canJump = true;
    }

    IEnumerator Blink()
    {
        look.sprite = eyes[3];
        yield return new WaitForSeconds(blinkTime);
        look.sprite = eyes[0];
    }
}
