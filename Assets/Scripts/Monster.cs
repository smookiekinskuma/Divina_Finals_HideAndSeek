using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject misty;

    [Header("Radar")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private float magnitude;
    [SerializeField] private Vector2 normalize;
    [SerializeField] private float dotProduct;
    [SerializeField] private float test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detection();
    }

    void Detection()
    {
        //Enemy and Enemy/Right
        Vector2 enemyDirection = (transform.right - transform.position);
        float enemyMagnitude = Mathf.Sqrt(Mathf.Pow(enemyDirection.x, 2) + Mathf.Pow(enemyDirection.y, 2));
        Vector2 enemyNormalize = enemyDirection / enemyMagnitude;

        //Player and Enemy
        direction = (transform.position - misty.transform.position);
        magnitude = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        normalize = direction / magnitude;

        //DotProduct
        dotProduct = (normalize.x * enemyNormalize.x) + (normalize.y * enemyNormalize.y);

        if (dotProduct < -0.7f)
        {
            Debug.Log("front");
            if (magnitude < 2)
            {
                Debug.Log("wow");
            }
            if (magnitude < 5)
            {
                Debug.Log("wowww");
            }
            if (magnitude < 13)
            {
                Debug.Log("wowwwwwww");
            }
        } else
        {
            Debug.Log("behind");
        }
        
    }

}
