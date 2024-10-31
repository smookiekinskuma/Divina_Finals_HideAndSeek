using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject misty;
    [SerializeField] private Misty mistyScript;

    [Header("Radar")]
    [SerializeField] private float magnitude;

    [Header("Hiding")]
    [SerializeField] private GameObject Indicator;

    void Start()
    {
        mistyScript = misty.GetComponent<Misty>();
    }

    // Update is called once per frame
    void Update()
    {
        Detection();
        Condition();
    }

    void Condition()
    {
        if (magnitude < 0.7)
        {
            Indicator.SetActive(true);
            if (!mistyScript.isHiding)
            { 
                Hide();
            }
            if (mistyScript.isHiding)
            {
                Leave();
            }
        } else {
            Indicator.SetActive(false);
        }
    }

    void Detection()
    {
        //Player and Cabinet
        Vector2 direction = (transform.position - misty.transform.position);
        magnitude = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
    }

    void Hide()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            misty.SetActive(false);
            misty.transform.position = transform.position;
            mistyScript.isHiding = true;
        }
    }

    void Leave()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            misty.SetActive(true);
            mistyScript.isHiding = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.7f);
    }
}
