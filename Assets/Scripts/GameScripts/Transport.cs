using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject misty;
    [SerializeField] private Misty misty_script;
    [SerializeField] private GameObject camera;

    [Header("Radar")]
    [SerializeField] private float magnitude;

    [Header("Hiding")]
    [SerializeField] private Transform nextLevel;
    [SerializeField] private GameObject Indicator;

    // Start is called before the first frame update
    void Start()
    {
        Indicator.SetActive(false);
        misty_script = misty.GetComponent<Misty>();
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
            misty_script.Checkpoint += 1;
            misty.transform.position = nextLevel.transform.position;
            camera.transform.position = nextLevel.transform.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.7f);
    }
}
