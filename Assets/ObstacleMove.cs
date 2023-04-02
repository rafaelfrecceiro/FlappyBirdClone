using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        if (!FindObjectOfType<PlayerMovement>().isDead)
        {
            transform.Translate(new Vector3(-1 * speed * Time.deltaTime,0,0));
        }
    }
}
