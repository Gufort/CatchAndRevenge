using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    private void Start()
    {
        // Устанавливаем громкость из PlayerPrefs или по умолчанию
        float volume = PlayerPrefs.GetFloat("BackgroundVolume", 1f);
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("BackgroundVolume", volume);
    }
}