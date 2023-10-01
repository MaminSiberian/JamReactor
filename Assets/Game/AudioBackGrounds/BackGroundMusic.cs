using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioClip[] bgMusic;
    private AudioSource _au;

    private void Start()
    {
        _au = GetComponent<AudioSource>();
        _au.PlayOneShot(bgMusic[Random.Range(0, bgMusic.Length - 1)]);
    }
    private void Update()
    {
        if(!_au.isPlaying)
        {
            _au.PlayOneShot(bgMusic[Random.Range(0, bgMusic.Length - 1)]);
        }
    }
}
