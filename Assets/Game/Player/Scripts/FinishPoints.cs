using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoints : MonoBehaviour, ICanCatching
{
    [SerializeField] private float timeWaitToVictory;
    [SerializeField] private int scenID;
    [SerializeField] private float minMaxRandomX, minMaxRandomY;    
    [SerializeField] private GameObject textVictory;
    [SerializeField] private GameObject[] particles;
    [SerializeField] public AudioClip victorySound;
    private AudioSource _as;
    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(1);
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        textVictory.SetActive(true);
        _as.PlayOneShot(victorySound);
        foreach(var particle in particles)
        {
            var randX = Random.Range(-minMaxRandomX, minMaxRandomX);
            var randY = Random.Range(-minMaxRandomY, minMaxRandomY);
            particle.transform.localPosition = new Vector2(randX, randY) ;
            particle.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(timeWaitToVictory);
        //SceneManager.LoadScene(scenID);

        GameDirector.PlayNextRoomEvent();
    }
}
