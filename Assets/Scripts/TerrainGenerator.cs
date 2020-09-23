using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain terrainPrefab;
    [Range(0, 20)]
    public int descendingThreshold = 1;
    [Range(0, 20)]
    public int ascendingThreshold = 1;
    public int terrainLenght = 10;

    Coroutine terrainRoutine;

    public void GenerateTerrain()
    {
        if (terrainRoutine != null)
        {
            StopCoroutine(terrainRoutine);
            terrainRoutine = null;
        }

        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (var terrain in terrains)
        {
            terrain.enabled = false;
        }

        terrainRoutine = StartCoroutine(GenerateTerrainRoutine());
    }

    private IEnumerator GenerateTerrainRoutine()
    {
        //Vector3 currentPosition = Vector3.zero;
        Vector3 currentPosition = FindObjectOfType<Player>().transform.position - Vector3.right * 1f;

        for (int i = 0; i < terrainLenght; i++)
        {
            Instantiate(terrainPrefab, currentPosition, Quaternion.identity);
            
            //UNDERGROUND
            for (int j = 0; j < 100; j++)
            {
                Instantiate(terrainPrefab, new Vector3(currentPosition.x, currentPosition.y - j + 1, 0), Quaternion.identity);
            }

            currentPosition += new Vector3(1, RandomOffsetY, 0);
            yield return null;
        }
    }

    private int RandomOffsetY
    {
        get
        {
            return UnityEngine.Random.Range(-descendingThreshold, ascendingThreshold + 1);
        }
    }
}
