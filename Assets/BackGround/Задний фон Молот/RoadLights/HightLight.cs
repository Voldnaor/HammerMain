using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : MonoBehaviour
{
    public float fadeDuration = 2f; // ƒлительность затухани€ и восстановлени€ бликов
    private float timer = 0f;
    private Renderer lightRenderer;
    private Color higltLightAlpha;
    private bool fadeInProgress = false;

    void Start()
    {
        lightRenderer = GetComponent<Renderer>();
        higltLightAlpha = lightRenderer.material.color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!fadeInProgress && timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration); // ”меньшает прозрачность бликов
            Color newColor = new Color(higltLightAlpha.r, higltLightAlpha.g, higltLightAlpha.b, alpha);
            lightRenderer.material.color = newColor;
        }
        else if (timer < 2 * fadeDuration)
        {
            fadeInProgress = true;
            float alpha = Mathf.Lerp(0f, 1f, (timer - fadeDuration) / fadeDuration); // ¬осстанавливает прозрачность бликов
            Color newColor = new Color(higltLightAlpha.r, higltLightAlpha.g, higltLightAlpha.b, alpha);
            lightRenderer.material.color = newColor;
        }
        else
        {
            timer = 0f;
            fadeInProgress = false;
        }
    }
}
