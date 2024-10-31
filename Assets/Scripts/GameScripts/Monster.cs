using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject misty;
    [SerializeField] private GameObject mistyPoint;
    [SerializeField] private Misty misty_script;

    [Header("Chase")]
    [SerializeField] public bool isSpotted;

    [Header("Radar")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private float magnitude;
    [SerializeField] private Vector2 normalize;
    [SerializeField] private float dotProduct;
    [SerializeField] private float test;

    [Header("Time")]
    [SerializeField] private float time;

    [Header("Knife")]
    [SerializeField] private GameObject knife;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    [SerializeField] private Transform p3;

    

    // Start is called before the first frame update
    void Start()
    {
        misty_script = misty.GetComponent<Misty>();
        isSpotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Conditions();
        Detection();
    }

    void Conditions()
    {
        //Spotting you
        if (dotProduct > 0.7f && magnitude < 5 && !misty_script.isHiding)
        {
            //He can see you now
            isSpotted = true;
            KnifeRaise();
            
        }

        //Losing track of you
        if (magnitude > 10)
        {
            isSpotted = false;
        }

        //Hiding
        if (misty_script.isHiding && magnitude > 5)
        {
            isSpotted = false;
        }

        //Dead
        if (magnitude < 1.2f && isSpotted)
        {
            GameOver();
        }
    }

    void Detection()
    {
        //Enemy and Enemy/Right
        Vector2 enemyDirection = (transform.right - transform.position);
        float enemyMagnitude = Mathf.Sqrt(Mathf.Pow(enemyDirection.x, 2) + Mathf.Pow(enemyDirection.y, 2));
        Vector2 enemyNormalize = enemyDirection / enemyMagnitude;

        //Player and Enemy
        direction = (transform.position - mistyPoint.transform.position);
        magnitude = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        normalize = direction / magnitude;

        //DotProduct
        dotProduct = (normalize.x * enemyNormalize.x) + (normalize.y * enemyNormalize.y); 
    }

    void KnifeRaise()
    {
        var p32 = Vector2.Lerp(p3.transform.position, p2.transform.position, magnitude / 6);
        var p21 = Vector2.Lerp(p2.transform.position, p1.transform.position, magnitude / 6);

        knife.transform.position = Vector2.Lerp(p32, p21, magnitude / 5);
    }

    void GameOver()
    {
        misty_script.Card.SetActive(true);
        misty_script.Text_update = "Game Over...";
        misty_script.OtherText_update = "'X' to respawn";

        if (Input.GetKeyDown(KeyCode.X))
        {
            misty_script.RestartLevel();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
