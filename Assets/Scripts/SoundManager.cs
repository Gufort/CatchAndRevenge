using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Slider volumeSlider;
    private const string volumeKey = "BackgroundVolume";

    private void Start()
    {
        LoadVolume();
        volumeSlider.onValueChanged.AddListener(SetVolume);
        float volume = PlayerPrefs.GetFloat("BackgroundVolume", 1f);
        SetVolume(volume);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(volumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(volumeKey);
            volumeSlider.value = savedVolume;
        }
        else
        {
            volumeSlider.value = 1f;
        }
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat(volumeKey, volume);
        PlayerPrefs.Save();
    }
}