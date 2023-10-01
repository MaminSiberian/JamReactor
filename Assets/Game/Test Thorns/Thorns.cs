using UnityEngine;

public class Thorns : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("dead");

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
