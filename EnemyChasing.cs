using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    public float MovementSpeed = 3f;
    public float TurningSpeed = 3f;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

   
    void Update()
    {
         Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TurningSpeed);
        transform.position += transform.forward * Time.deltaTime * MovementSpeed;
    }
}
