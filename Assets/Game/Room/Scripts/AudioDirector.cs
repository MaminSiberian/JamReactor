using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    [SerializeField] private AudioSource vaseSound;
    [SerializeField] private AudioSource TVEventSound;
    [SerializeField] private AudioSource catSound;
    [SerializeField] private AudioSource crackSound;

    public void PlayVaseSound()
    {
        vaseSound.Play();
    }
    public void PlayTVEventSound()
    {
        TVEventSound.Play();
    }
    public void PlayCatSound()
    {
        catSound.Play();
    }
    public void PlayCrackSound()
    {
        crackSound.Play();
    }
}
