using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float timeToSpawn;
    [SerializeField] private Transform[] spawnPonts;
    [SerializeField] private GameObject enemy;

    public float speed;

    void Start()
    {
        Spawn();
        StartCoroutine(Timer());
    }

    private void Update()
    {

    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
            Spawn();
            //Debug.Log("Spawn");
        }
    }

    private void Spawn()
    {
        int indexToSpawn = Random.Range(0, spawnPonts.Length);

        GameObject spawnedObject = Instantiate(enemy);
        spawnedObject.transform.position = spawnPonts[indexToSpawn].position;
        //spawnedObject.GetComponent<SkeletonAnimation>().gameObject.layer = spawnPonts[indexToSpawn].GetComponent<SpawnPoint>().LayerNumber;
        spawnedObject.GetComponent<MeshRenderer>().sortingOrder = spawnPonts[indexToSpawn].GetComponent<SpawnPoint>().LayerNumber;
    }
}
