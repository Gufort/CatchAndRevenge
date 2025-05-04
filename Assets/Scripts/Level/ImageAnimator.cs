using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    public Sprite[] frames; 
    public float frameDelay = 0.7f; 
    public float fadeDuration = 1.0f;

    public bool animationFinished = false;

    public Image imageComponent; 
    public AudioClip soundClip;
    private AudioSource audioSource; 

    void Start()
    {
        animationFinished = false;
        audioSource = GetComponent<AudioSource>();
        imageComponent = GetComponent<Image>(); 
        imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, 0);
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        yield return StartCoroutine(FadeIn());

        for (int i = 0; i < frames.Length; i++)
        {
            imageComponent.sprite = frames[i];

            if (audioSource != null && soundClip != null)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
            }
            yield return new WaitForSeconds(frameDelay); 
        }
        animationFinished = true;
        yield return new WaitForSeconds(5);
        imageComponent.enabled = false;
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = imageComponent.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); 

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageComponent.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration); 
            yield return null; 
        }

        imageComponent.color = endColor; 
    }
}