using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

public class EyeHandle : MonoBehaviour
{
    [SerializeField] private bool open;
    [SerializeField] private bool lClose;
    [SerializeField] private bool rClose;
    [SerializeField] private bool bClose;
    [SerializeField] private bool message = false;
    [SerializeField] private Canvas hiddenMessage;
    [SerializeField] private bool cube;
    [SerializeField] private Rigidbody2D cubeGravity;

    private Player player;
    private SpriteRenderer thisRenderer;
    private BoxCollider2D thisCollider;

    private bool hide;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (message) return;
        thisRenderer = GetComponent<SpriteRenderer>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hide = true;
        if (open && hide)
        {
            if (!player.closeLeft && !player.closeRight)
            {
                hide = false;
            }
        }
        if (lClose && hide)
        {
            if (player.closeLeft && !player.closeRight)
            {
                hide = false;
            }
        }
        if (rClose && hide)
        {
            if (!player.closeLeft && player.closeRight)
            {
                hide = false;
            }
        }
        if (bClose && hide)
        {
            if (player.closeLeft && player.closeRight)
            {
                hide = false;
            }
        }

        if (hide)
        {
            if (message) hiddenMessage.enabled = false;
            else
            {
                thisRenderer.enabled = false;
                thisCollider.enabled = false;
                if (!cube) return;
                cubeGravity.gravityScale = 0;
                cubeGravity.velocity = Vector2.zero;
            }
        }
        else
        {
            if (message) hiddenMessage.enabled = true;
            else
            {
                thisRenderer.enabled = true;
                thisCollider.enabled = true;
                if (cube) cubeGravity.gravityScale = 5;
            }
        }
    }
}
