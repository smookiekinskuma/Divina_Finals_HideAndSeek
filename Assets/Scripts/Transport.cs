using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject misty;

    [Header("Radar")]
    [SerializeField] private float magnitude;

    [Header("Hiding")]
    [SerializeField] private Transform nextLevel;
    [SerializeField] private GameObject Indicator;

    // Start is called before the first frame update
    void Start()
    {
        Indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Detection();
        Condition();
    }

    void Condition()
    {
        if (magnitude < 0.5)
        {
            Indicator.SetActive(true);
            Transporting();
        }
        else
        {
            Indicator.SetActive(false);
        }
    }

    void Detection()
    {
        Vector2 direction = (transform.position - misty.transform.position);
        magnitude = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
    }

    void Transporting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            misty.transform.position = nextLevel.transform.position;
        }
    }
}
