using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CoinsSpawn : MonoBehaviour
{
    public GameObject coin;
    public int spawnProbability;

    private void Start()
    {
        StartCoroutine($"Spawn");
        if (spawnProbability == 0)
        {
            spawnProbability = 10;
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (UnityEngine.Random.Range(1, spawnProbability) == 1)
            {
                var x = UnityEngine.Random.Range(-16, 16);
                var position = new Vector3(x, transform.position.y, transform.position.z);
                var newPosition = position;
                Instantiate(coin, newPosition, coin.transform.rotation, gameObject.transform);
            }
        }
    }
}
