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
        foreach(var particle in particles)
        {
            var randX = Random.Range(-minMaxRandomX, minMaxRandomX);
            var randY = Random.Range(-minMaxRandomY, minMaxRandomY);
            particle.transform.localPosition = new Vector2(randX, randY) ;
            particle.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(timeWaitToVictory);
        SceneManager.LoadScene(scenID);
    }
}
