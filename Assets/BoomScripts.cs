using System.Collections;
using UnityEngine;

public class BoomScripts : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);
    }

    
}
