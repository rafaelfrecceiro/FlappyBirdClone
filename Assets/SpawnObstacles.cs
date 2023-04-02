using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public float spawnTime = 4f;
    private float time = 0f;
    public GameObject obstacle;

    public GameObject coin;
    private float timeCoin = 0f;
    public float spawnTimeCoin = 6f;
    
    void Update()
    {
        if (!FindObjectOfType<PlayerMovement>().isDead)
        {
            time += Time.deltaTime;
            timeCoin += Time.deltaTime;
            
            if (time >= spawnTime)
            {
                time = 0f;
                int posY = Random.Range(14, 17);
                Instantiate(obstacle, new Vector3(transform.position.x,posY,0), Quaternion.identity);
            }

            if (timeCoin >= spawnTimeCoin)
            {
                timeCoin = 0f;
                int posCoinY = Random.Range(2, 9);
                Instantiate(coin, new Vector3(transform.position.x,posCoinY,0), Quaternion.identity);
            }
        }
    }
}
