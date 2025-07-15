using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject []obstaclePrefabs;

    [SerializeField]
    private GameObject obstacleParent;

    int obstacleAmount = 20; // Number of obstacles to generate  

   
    void Start()
    {
        StartCoroutine("CreateObstacle");
    }

    IEnumerator CreateObstacle()
    {
        yield return new WaitForSeconds(2.5f); // Wait for 2.5 seconds before creating the next obstacle  

        while (obstacleAmount > 0)
        {
            var obstaclePrefab = obstaclePrefabs[UnityEngine.Random.Range(0, obstaclePrefabs.Length)];
            float spawnPositionX = UnityEngine.Random.Range(-3.0f, 3.0f);
            Vector3 newPosition = new Vector3(spawnPositionX, transform.position.y, transform.position.z);

            Instantiate(obstaclePrefab, newPosition, UnityEngine.Random.rotation, obstacleParent.transform);
            obstacleAmount--;
            yield return new WaitForSeconds(2.5f); // Wait for 2.5 seconds before creating the next obstacle  
        }
    }

    // Update is called once per frame  
    void Update()
    {
    }
}
