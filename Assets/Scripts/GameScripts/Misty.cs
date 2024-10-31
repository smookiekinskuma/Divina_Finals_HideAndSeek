using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Misty : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject camera;

    [Header("Moving")]
    [SerializeField] private float move;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private bool flip;

    [Header("Hiding")]
    [SerializeField] public bool isHiding;

    [Header("Card")]
    [SerializeField] public GameObject Card;
    [SerializeField] public TextMeshProUGUI Text;
    [SerializeField] public TextMeshProUGUI OtherText;
    [SerializeField] public string Text_update;
    [SerializeField] public string OtherText_update;

    [Header("Checkpoint")]
    [SerializeField] public int Checkpoint;
    [SerializeField] private Transform []CheckpointLocation;

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Card.SetActive(false);
        move = 5f;
        isHiding = false;
        Checkpoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = Text_update;
        OtherText.text = OtherText_update;

        Movement();
        Condition();
    }

    void Condition()
    {
        if (Checkpoint >= 3)
        {
            Escaping();
        }
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

    void Escaping()
    {
        Card.SetActive(true);
        Text_update = "You Escaped!";
        OtherText_update = "'X' to go back to the main menu!";

        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void RestartLevel()
    {
        Card.SetActive(false);
        switch (Checkpoint)
        {
            case 0: //First Stage
                camera.transform.position = CheckpointLocation[0].transform.position;
                transform.position = CheckpointLocation[0].transform.position;
                break;
            case 1: //Second Stage
                camera.transform.position = CheckpointLocation[1].transform.position;
                transform.position = CheckpointLocation[1].transform.position;
                break;
            case 2: //Third Stage
                camera.transform.position = CheckpointLocation[2].transform.position;
                transform.position = CheckpointLocation[2].transform.position;
                break;
        }
    }
}
