using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject waterPrefab; // Префаб спрайта воды
    public Transform leftPoint; // Левая точка
    public Transform rightPoint; // Правая точка
    public float flowSpeed = 2f; // Скорость движения воды
    private GameObject currentWater;

    void Start()
    {
        CreateWaterSprite(leftPoint.position);
    }

    void Update()
    {
        if (currentWater != null)
        {
            currentWater.transform.Translate(Vector3.right * flowSpeed * Time.deltaTime);

            if (currentWater.transform.position.x >= rightPoint.position.x)
            {
                Destroy(currentWater);
                CreateWaterSprite(leftPoint.position);
            }
        }
    }

    void CreateWaterSprite(Vector3 position)
    {
        currentWater = Instantiate(waterPrefab, position, Quaternion.identity);
    }
}
