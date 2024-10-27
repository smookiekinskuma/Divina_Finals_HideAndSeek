using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misty : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float move;
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        move = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveX = Input.GetAxisRaw("Horizontal") * move;
        moveY = Input.GetAxisRaw("Vertical") * move;
        rb.velocity = new Vector2(moveX, moveY);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            
        } else
        {

        }

    }
}
