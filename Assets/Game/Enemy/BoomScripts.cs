using System.Collections;
using UnityEngine;

public class BoomScripts : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public AudioClip[] deathSound;
    
    void Start()
    {
        StartCoroutine(Timer());
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.PlayOneShot(deathSound[Random.Range(0,deathSound.Length - 1)]);
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);
    }

    
}
