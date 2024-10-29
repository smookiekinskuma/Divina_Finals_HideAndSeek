using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform misty;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        speed = 20f;
        offset.z = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 permanent = misty.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, permanent, speed * Time.deltaTime);
        transform.position = smooth;
    }
}
