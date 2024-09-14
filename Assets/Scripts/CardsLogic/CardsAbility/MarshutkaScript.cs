using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshutkaScript : MonoBehaviour
{
    public GameObject marhrutka;
    public Transform[] linePoints;

    public PlayerController playerController;
    
    private void Start()
    {
        //Массив точек спавна
        linePoints = new Transform[6];
        linePoints[0] = GameObject.Find("RightPoint_layer1").transform;
        linePoints[1] = GameObject.Find("RightPoint_layer2").transform;
        linePoints[2] = GameObject.Find("RightPoint_layer3").transform;
        linePoints[3] = GameObject.Find("LeftPoint_layer1").transform;
        linePoints[4] = GameObject.Find("LeftPoint_layer2").transform;
        linePoints[5] = GameObject.Find("LeftPoint_layer3").transform;

        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    public void SpawnPrefabAtRandomPoint()
    {
        //Выбор одной из точек спавна (совпадают с точками спавна врагом, т.е. на линии)
        int randomIndex = Random.Range(0, linePoints.Length);
        Transform spawnPoint = linePoints[randomIndex];

        //Спавн маршрутки
        GameObject spawnedObject = Instantiate(marhrutka, spawnPoint.position, Quaternion.identity);
        //Debug.Log(spawnPoint.name);

        playerController.marshrutkaCardsPoint++;

        // Поворот спрайта в зависимости от точки спавна
        if (randomIndex >= 3 && randomIndex <= 5)
        {
            spawnedObject.transform.localScale = new Vector3(-0.124981f, 0.124981f, 0.124981f); //Спайп если в левой части экрана справн произошел
        }
    }
    
}
