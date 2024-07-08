using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stwarzacz : MonoBehaviour
{
    [SerializeField]
    private Transform cubePos;
    [SerializeField]
    private GameObject stwarwzaczPrefab;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private bool isSpawned = false;

    private float distance;
    private GameObject spawnedSimba;

    void Start()
    {
        // Validate and initialize positions if necessary
        if (cubePos == null)
        {
            Debug.LogError("cubePos is not assigned!");
        }

        if (player == null)
        {
            Debug.LogError("player is not assigned!");
        }
    }

    void Update()
    {
        if (cubePos == null || player == null)
            return;

    //    distance = Vector3.Distance(player.position, cubePos.position);
      distance=  Mathf.Sqrt(Mathf.Pow(player.position.x - cubePos.position.x, 2) +  Mathf.Pow(player.position.z - cubePos.position.z, 2));

        if (distance < 100 && !isSpawned)
        {
            spawnedSimba = Instantiate(stwarwzaczPrefab, cubePos.position, Quaternion.identity);
            isSpawned = true;
        }
        else if (distance > 100 && isSpawned)
        {
          spawnedSimba.SetActive(false);
         //   isSpawned = false;
        }
        else if(distance <100 && isSpawned) 
        {
            spawnedSimba.SetActive(true);
        
        }
    }
}
