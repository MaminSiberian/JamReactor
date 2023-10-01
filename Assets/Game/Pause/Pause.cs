using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject elements;
    [SerializeField] private AudioMixerGroup mixer;

    private void Awake()
    {
        elements.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    private void TogglePause()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
            elements.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            elements.SetActive(false);
        }
    }
    public void SetMusicVolume(float value)
    {
        mixer.audioMixer.SetFloat("MusicVolume", value);
    }
    public void SetEffectsVolume(float value)
    {
        mixer.audioMixer.SetFloat("EffectsVolume", value);
    }
}
