using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    [SerializeField] private AudioSource vaseSound;
    [SerializeField] private AudioSource TVEventSound;


    public void PlayVaseSound()
    {
        vaseSound.Play();
    }
    public void PlayTVEventSound()
    {
        TVEventSound.Play();
    }
}
