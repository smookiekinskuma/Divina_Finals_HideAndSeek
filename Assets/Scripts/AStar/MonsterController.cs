using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("AStar")]
    [SerializeField] public AStar AStar; //Astar
    [SerializeField] public Node currentNode; //Nodes
    [SerializeField] public List<Node> path; //Path

    [Header("Scripts")]
    [SerializeField] public GameObject misty;
    [SerializeField] public Monster Monster;

    [Header("Speed")]
    [SerializeField] private float Chasespeed = 4f;
    [SerializeField] private float Roamspeed = 0.5f;
    [SerializeField] private float speed; //Current Speed

    [Header("Other")]
    [SerializeField] public bool isClear; //For chasing
    [SerializeField] private Vector2 prevPosition; //For rotating


    void Start()
    {
        Monster = GetComponent<Monster>();
        path = new List<Node>();
        isClear = true;
    }

    private void Update()
    {
        if (Monster.isSpotted == false) //Roaming
        {
            Roam();
            isClear = true;
        }
        else if (Monster.isSpotted == true) //Chasing
        {
            if (isClear == true)
            {
                path.Clear();
                isClear = false;
            }
            Chase();
        }

        CreatePath();
        LeftorRight();
    }

    void Roam()
    {
        if (path == null || path.Count == 0)
        {
            //Choosing a random target
            Node targetNode = AStar.NodesInScene()[Random.Range(0, AStar.NodesInScene().Length)];

            //Update currentNode to the monster's current position
            currentNode = AStar.Path(transform.position);

            // Generate a path to said random location
            path = AStar.GeneratePath(currentNode, targetNode); 

            speed = Roamspeed;
        }
    }

    void Chase()
    {
        if (path == null || path.Count == 0)
        {
            //Update currentNode to the monster's current position
            currentNode = AStar.Path(transform.position);

            //Find the nearest node to Misty
            Node targetNode = AStar.Path(misty.transform.position); 

            //Generate the path to Misty
            path = AStar.GeneratePath(currentNode, targetNode); 

            speed = Chasespeed;
        }
    }

    void CreatePath()
    {
        if (path != null && path.Count > 0)
        {
            //Move towards the current target node in the path
            Node targetNode = path[0];
            transform.position = Vector2.MoveTowards(transform.position, targetNode.transform.position, speed * Time.deltaTime);

            //Enemy and targetNode
            Vector2 nodeDirection = (transform.position - targetNode.transform.position);
            float nodeDistance = Mathf.Sqrt(Mathf.Pow(nodeDirection.x, 2) + Mathf.Pow(nodeDirection.y, 2));

            // Check if the monster has reached the target node
            if (nodeDistance < 0.1f)
            {
                // Remove the reached node from the path
                path.RemoveAt(0);
            }
        }
    }

    void LeftorRight()
    {
        //When right
        if (transform.position.x > prevPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //When left
        else if (transform.position.x < prevPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //Update
        prevPosition = transform.position;
    }
}
