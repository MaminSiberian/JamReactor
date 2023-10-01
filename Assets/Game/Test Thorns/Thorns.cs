using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorns : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("TwoLevel");

        }
        if(collision.gameObject.CompareTag("Enemy") )
        {
            if(collision.gameObject.GetComponent<EnemyController>().isFall)
            {
                Destroy(collision.gameObject);
            }
        }
    }
    
}
