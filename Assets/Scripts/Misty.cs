using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misty : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float move;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private bool flip;

    [Header("Hiding")]
    [SerializeField] public bool isHiding;

    [Header("GameOver")]
    [SerializeField] public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        move = 5f;
        isHiding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //Moving
        float moveX = Input.GetAxisRaw("Horizontal") * move;
        float moveY = Input.GetAxisRaw("Vertical") * move;
        rb.velocity = new Vector2(moveX, moveY);
        
        //Animation
        if (moveX != 0 || moveY != 0)
        { anim.GetComponent<Animator>().Play("Walking"); } 
        else
        { anim.GetComponent<Animator>().Play("Idle"); }

        //Flip the character
        if (moveX > 0 && !flip) 
        { transform.Rotate(0f, 180f, 0f); flip = !flip; }
        if (moveX < 0 && flip) 
        { transform.Rotate(0f, 180f, 0f); flip = !flip; }
    }
}
