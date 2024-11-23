using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Nodes")]
    public Node cameFrom;
    public List<Node> connections;

    [Header("Scores")]
    public float gScore; //Cost from the start
    public float hScore; //Estimated cost to the end

    public void Start()
    {
        SetNeighbors();
    }

    public float FScore()
    {
        return gScore + hScore;
    }

    private void SetNeighbors()
    {
        connections.Clear();
        var overlapObjects = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f); //Detecting neighboring nodes

        foreach (Collider2D objects in overlapObjects)
        {
            if (objects.gameObject == this.gameObject)
               continue;

            Node neighborNode = objects.gameObject.GetComponent<Node>();
            if (neighborNode != null)
            {
                connections.Add(neighborNode);
            }
        }
    }

    private void OnDrawGizmos() //Turn this on and EVERYTHING will lag lol.
    {
        Gizmos.color = Color.blue;

        // Draw lines to connected nodes
        foreach (Node connection in connections)
        {
            if (connection != null)
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
    }
}
