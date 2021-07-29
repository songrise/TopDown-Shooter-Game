using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnObject;
    public Transform spawnSpot;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(spawnObject, spawnSpot.position, Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
}
