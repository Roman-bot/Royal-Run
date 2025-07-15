using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fence : MonoBehaviour
{
    [SerializeField]
    GameObject fencePrefab;

    [SerializeField]
    GameObject applePrefab;

    [SerializeField]
    GameObject coinPrefab;

    float coinSpawnOffset = 2.0f; // Offset to spawn coins slightly in front of the fence

    int []sideCoordinate = { -3, 0, 3 }; // Coordinates for the left and right sides of the fence
    int []fenceAmount = { 1, 2, 3 }; // Possible amounts of fences to spawn
    List<int> avalibleLane = new List<int> { 0, 1, 2 };

    float spawnAppleWeight = 0.8f; // Probability of spawning an apple


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
        SpawnsFence();
        SpawnApple();
        SpawnCoin();
    }

    private void SpawnCoin()
    {
        if (Random.value > spawnAppleWeight) return; // If the random value is greater than the weight, exit the method
        if (avalibleLane.Count <= 0) return; // If no lanes are available, exit the method

        int selectedLane = selectLane();

        int coinsToSpawn = Random.Range(3, 5);

        float zTopChunk = transform.position.z + coinSpawnOffset; // Offset to spawn coins slightly in front of the fence

        for (int i = 0; i< coinsToSpawn; i++)
        {
            float zCoordinate = zTopChunk - i * coinSpawnOffset; // Adjust the z-coordinate for each coin to prevent overlap
            Vector3 position = new Vector3(sideCoordinate[selectedLane], transform.position.y, zCoordinate);
            Instantiate(coinPrefab, position, Quaternion.identity, this.transform);
        }

    }


    private void SpawnApple()
    {
        if (Random.value > spawnAppleWeight) return; // If the random value is greater than the weight, exit the method
        if (avalibleLane.Count <= 0) return; // If no lanes are available, exit the method
        
        int selectedLane = selectLane();
   
        Vector3 position = new Vector3(sideCoordinate[selectedLane], transform.position.y, transform.position.z);
        Instantiate(applePrefab, position, Quaternion.identity, this.transform); // исплозуем другой метод для сета к паренту, чтобы яблоко прилипло к чанку
    }

  
    void SpawnsFence()
    {
        int fenceAmount = Random.Range(0, this.fenceAmount.Length);
       
        for (int i = 0; i < fenceAmount; i++)
        {
            if(avalibleLane.Count <= 0) break; // If no lanes are available, exit the loop

            int selectedLane = selectLane();
            //Debug.Log("Selected fence lane: " + selectedLane);
            Vector3 fencePosition = new Vector3(sideCoordinate[selectedLane], transform.position.y, transform.position.z);
            var fence = Instantiate(fencePrefab, fencePosition, Quaternion.identity, this.transform);

            /*
            if (fence != null && fence.transform != null)
            {
                fence.transform.SetParent(this.transform, true);
            }
            */
           
        }
        
    }
    int selectLane()
    {
        int randomIndex = Random.Range(0, avalibleLane.Count);
        int selecteLane = avalibleLane[randomIndex];
        avalibleLane.RemoveAt(randomIndex);
        return selecteLane;
    }
}
