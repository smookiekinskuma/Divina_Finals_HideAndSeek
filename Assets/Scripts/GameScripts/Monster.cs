using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Player/Misty")]
    [SerializeField] private GameObject misty;
    [SerializeField] private GameObject mistyPoint; //
    [SerializeField] private Misty misty_script;
    [SerializeField] public bool isSpotted;
    [SerializeField] private bool isDead;    

    [Header("Self")]
    [SerializeField] private MonsterController controller;
    [SerializeField] private Vector2 startingPoint;

    [Header("Radar")]
    [SerializeField] private Transform front;
    [SerializeField] private float magnitude;
    [SerializeField] private float dotProduct;

    [Header("Knife")]
    [SerializeField] private GameObject knife;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    [SerializeField] private Transform p3;

    // Start is called before the first frame update
    void Start()
    {
        misty_script = misty.GetComponent<Misty>();

        startingPoint = transform.position;
        isSpotted = false;
        isDead = false;
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
            isDead = true;
        }

        if (isDead == true)
        {
            GameOver();
            Restart();
            controller.path.Clear();
        }
    }

    void Detection() //Detecting Misty
    {
        //Enemy and Enemy/Front
        Vector2 enemyDirection = (front.transform.position - transform.position);
        float enemyMagnitude = Mathf.Sqrt(Mathf.Pow(enemyDirection.x, 2) + Mathf.Pow(enemyDirection.y, 2));
        Vector2 enemyNormalize = enemyDirection / enemyMagnitude;

        //Player and Enemy
        Vector2 direction = (transform.position - mistyPoint.transform.position);
        magnitude = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        Vector2 normalize = direction / magnitude;

        //DotProduct
        dotProduct = (normalize.x * enemyNormalize.x) + (normalize.y * enemyNormalize.y); 
    }

    void KnifeRaise() //This just raises the knife every time Misty is nearby. That's it.
    {
        var p32 = Vector2.Lerp(p3.transform.position, p2.transform.position, magnitude / 6);
        var p21 = Vector2.Lerp(p2.transform.position, p1.transform.position, magnitude / 6);

        knife.transform.position = Vector2.Lerp(p32, p21, magnitude / 5);
    }

    void GameOver() //When dead
    {
        misty_script.Card.SetActive(true);
        misty_script.Text_update = "Game Over...";
        misty_script.OtherText_update = "'X' to respawn";

        if (Input.GetKeyDown(KeyCode.X)) //Restarting
        {
            misty_script.RestartLevel();
            isDead = false;
        }
    }

    void Restart() //Restarting ALL MONSTERS back to their original position.
    {
        // Find all GameObjects in the scene
        GameObject[] allMonsters = GameObject.FindObjectsOfType<GameObject>();

        // Loop through all GameObjects
        foreach (GameObject monster in allMonsters)
        {
            // Check if the GameObject is on the "Monster" layer
            if (monster.layer == LayerMask.NameToLayer("Monster"))
            {
                // Get the Monster script component
                Monster OthermonsterScript = monster.GetComponent<Monster>();
                if (OthermonsterScript != null)
                {
                    // Call the function in the Monster script
                    OthermonsterScript.transform.position = OthermonsterScript.startingPoint;
                    OthermonsterScript.controller.path.Clear();
                }
            }
        }
    }

    void OnDrawGizmos() //Circles and Lines
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.DrawWireSphere(transform.position, 10f);
        //Gizmos.DrawLine(transform.position, front.transform.position);
    }
}
