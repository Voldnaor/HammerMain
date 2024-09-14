using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonScript : MonoBehaviour
{
    public SpriteRenderer viveska;

    public float minBlinkInterval = 0.5f;//Интервал мигания
    public float maxBlinkInterval = 0.5f;//Интервал мигания
    public float nextBlinkTime;
    public float minIntensity = 0.5f; //Минимальная яркость
    public float maxIntensity = 1f; //Максимальная яркость


    private float timer = 0f;
    private bool isNeonOn = true;

    private void Start()
    {
        nextBlinkTime = Random.Range(minBlinkInterval, maxBlinkInterval);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextBlinkTime)
        {
            isNeonOn = !isNeonOn;
            timer = 0f;

            if (isNeonOn)
            {
                viveska.color = new Color(1f, 1f, 1f, Random.Range(minIntensity, maxIntensity)); //Рандомно выбирает из интервала яркости
            }
            else
            {
                viveska.color = new Color(1f, 1f, 1f, 0f);
            }
            nextBlinkTime = Random.Range(minBlinkInterval, maxBlinkInterval); //Рандомно выбирает из интервала мигания
        }
    }
}
