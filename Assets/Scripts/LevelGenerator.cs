using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    [Header("Chunk objects")]

    [SerializeField]
    GameObject []chunks;

    [SerializeField]
    GameObject chunkParent;

    [Header("Chunk settings")]
    [Tooltip("The amount of chunk!")]

    [SerializeField]
    int chunkAmount = 10;

    [SerializeField]
    int chunkLength = 10;

    [SerializeField]
    public float speed = 10.0f;

    GameObject[] chunksArray;
    List<GameObject> chunksList; 

    float speedChangeAmount = 2f;

    CameraController cameraController;
    int chunkSpawned = 0;

    [SerializeField]
    GameObject checkpointChunk;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //chunksArray = new GameObject[chunkAmount]
        chunksList = new List<GameObject>();
        CreateStartingChunks();
        cameraController = GameObject.Find("Virtual Camera").GetComponent<CameraController>();
    }

    public void ChunkSpeedUP()
    {
        speed += speedChangeAmount;
        if (speed > 20f)
        { 
            speed = 20f;
        }
        cameraController.ChangeCameraFOV(speedChangeAmount);
    }

    public void ChunkSlowDawn()
    {
        speed -= speedChangeAmount;
        if (speed < 2f)
        {
            speed = 2f;
        }
        cameraController.ChangeCameraFOV(-speedChangeAmount);
    }

    float CalculatePositionZ()
    {
        float positionZ = 0;

        if (chunksList.Count == 0)
        {
            positionZ = transform.position.z;
        }
        else
        {
            positionZ = chunksList[chunksList.Count-1].transform.position.z + chunkLength; //пермещаем в начало дороги, на место последнего чанка
        }

        return positionZ;
    }

    void CreateStartingChunks()
    {
        for (int i = 0; i < chunkAmount; i++)
        {
            CreateChunks();
        }
    }

    void CreateChunks()
    {
        var positionZ = CalculatePositionZ();
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, positionZ);
        GameObject chunkToSpawn = null; // Initialize the variable to avoid CS0165  

        if (chunkSpawned % 3 == 0 && chunkSpawned != 0)
        {
            chunkToSpawn = checkpointChunk; // Assign a value to chunkToSpawn  
            Debug.Log("Checkpoint chunk spawned at position: " + pos);
        }
        else
        {
            chunkToSpawn = chunks[UnityEngine.Random.Range(0, chunks.Length)]; // Assign a value to chunkToSpawn  
        }

        GameObject chunkForList = Instantiate(chunkToSpawn, pos, Quaternion.identity, chunkParent.transform);
        chunksList.Add(chunkForList);
        chunkSpawned++;
    }

    // Update is called once per frame
    void Update()
    {
        MoveChunks();
    }

    void MoveChunks()
    {
        for(int i = 0; i<chunksList.Count; i++)
        {
            var chunk = chunksList[i];
            chunk.transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunksList.Remove(chunk);
                Destroy(chunk);
                CreateChunks();

            }
        }
    }
}
