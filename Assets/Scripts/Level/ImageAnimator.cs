using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    public Sprite[] frames; // Массив спрайтов для анимации
    public float frameDelay = 0.7f; // Задержка между кадрами

    private Image imageComponent; // Компонент Image

    public AudioClip soundClip; // Аудиоклип для проигрывания
    private AudioSource audioSource; // Компонент AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
        imageComponent = GetComponent<Image>(); // Получаем компонент Image
        StartCoroutine(PlayAnimation()); // Запускаем корутину для анимации
    }

    private IEnumerator PlayAnimation()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            imageComponent.sprite = frames[i]; // Устанавливаем текущий кадр
            // Проигрываем звук
            if (audioSource != null && soundClip != null)
            {
                audioSource.clip = soundClip; // Устанавливаем аудиоклип
                audioSource.Play(); // Проигрываем звук
            }
            yield return new WaitForSeconds(frameDelay); // Ждем перед переходом к следующему кадру
        }

        imageComponent.enabled = false; // Прячем Image после завершения анимации
    }
}