using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar: MonoBehaviour
{
    [Header("Path")]
    public List<Node> currentPath;

    public List<Node> GeneratePath(Node start, Node end) //Generates path
    {
        List<Node> openSet = new List<Node>(); //Nodes to be evaluated

        foreach(Node node in FindObjectsOfType<Node>()) //Unvisited or Unreachable
        {
            node.gScore = float.MaxValue;
        }

        //gScore (Start)
        start.gScore = 0;

        //hScore (End)
        Vector2 hScoreDirection = (start.transform.position - end.transform.position);
        start.hScore = Mathf.Sqrt(Mathf.Pow(hScoreDirection.x, 2) + Mathf.Pow(hScoreDirection.y, 2));

        openSet.Add(start);

        while (openSet.Count > 0) //This loop will find the lowest fscore until it reaches the end node
        {
            int lowestF = default;

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore()) //Replaces previous lowest fScore
                {
                    lowestF = i;
                }
            }
            
            Node currentNode = openSet[lowestF]; //Current node with lowest fScore
            openSet.Remove(currentNode); //current Node will be evaluated

            if (currentNode == end) //Has found a path
            {
                List<Node> path = new List<Node>();
                path.Insert(0, end);

                while (currentNode != start) //Going back to the start
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }

                path.Reverse();
                currentPath = path; // Store the current path
                return path;
            }

            foreach (Node connectedNode in currentNode.connections) //Calculating gScore and hScores
            {
                //gScore
                Vector2 gScoreDirection = (currentNode.transform.position - connectedNode.transform.position);
                float gScorenodeDistance = Mathf.Sqrt(Mathf.Pow(gScoreDirection.x, 2) + Mathf.Pow(gScoreDirection.y, 2));

                float heldGScore = currentNode.gScore + gScorenodeDistance;

                if (heldGScore < connectedNode.gScore) //Finding the shortest path
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = heldGScore;

                    //hScore
                    Vector2 connectedDirection = (connectedNode.transform.position - end.transform.position);
                    connectedNode.hScore = Mathf.Sqrt(Mathf.Pow(connectedDirection.x, 2) + Mathf.Pow(connectedDirection.y, 2));

                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                }
            }
        }

        return null;
    }

    public Node Path(Vector2 pos) //Finding the starting and ending node
    {
        Node foundNode = null;
        float minDistance = float.MaxValue;

        foreach (Node node in FindObjectsOfType<Node>())
        {
            //Current Distance
            Vector2 currentDirection = (pos - (Vector2)node.transform.position);
            float currentDistance = Mathf.Sqrt(Mathf.Pow(currentDirection.x, 2) + Mathf.Pow(currentDirection.y, 2));

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                foundNode = node;
            }
        }

        return foundNode;
    }

    public Node[] NodesInScene() //Finds all of the nodes
    {
        return FindObjectsOfType<Node>();
    }

    private void OnDrawGizmos()
    {
        if (currentPath != null && currentPath.Count > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < currentPath.Count - 1; i++)
            {
                Gizmos.DrawLine(currentPath[i].transform.position, currentPath[i + 1].transform.position);
            }
        }
    }
}
