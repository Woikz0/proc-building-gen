using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProGen : MonoBehaviour
{
    public Transform parent;
    public Transform startPoint;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject windowPrefab;


    private List<List<GameObject>> layers = new List<List<GameObject>>();

    [Header("Generation Settings")]
    public int height = 1;
    public int width = 1;

    [Space(20f)]
    [Range(0, 1f)]
    public float doorPercentChance;

    [Range(0, 1f)]
    public float windowPercentChance;


    public void Generate()
    {
        Clear();

        Vector3 _startPoint = startPoint.position;

        // height loop
        for (int h = 0; h < height; h++)
        {
            _startPoint = new Vector3(_startPoint.x, startPoint.position.y + h, _startPoint.z);
            Vector3 lastInstantiatePos = _startPoint;

            layers.Add(new List<GameObject>());

            for (int i = 0; i < 4; i++) // 4-wall loop
            {
                Quaternion rot = Quaternion.Euler(0, i * -90, 0);

                for (int w = 0; w < width; w++) //width loop
                {
                    GameObject lastCreate = Instantiate(RandomWallType(h), lastInstantiatePos, rot);
                    Vector3 nextPoint = lastCreate.transform.position + lastCreate.transform.right;
                    lastInstantiatePos = nextPoint;

                    layers[h].Add(lastCreate);
                    lastCreate.transform.parent = parent.transform;
                }
                lastInstantiatePos = layers[h][layers[h].Count - 1].transform.position;
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }

    GameObject RandomWallType(int height)
    {
        float randomChance;
        if(height == 0){
            randomChance = Random.Range(0, 1f);
            if(randomChance < doorPercentChance) return doorPrefab;
            else return wallPrefab;
        }

        randomChance = Random.Range(0, 1f);
        if(randomChance < windowPercentChance) return windowPrefab;

        return wallPrefab;
    }

}
