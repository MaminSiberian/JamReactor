using UnityEngine;

public class Thorns : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision Enter");
           // collision.gameObject.GetComponent<Health>().Takedamage(damage);
        }
    }
    
}
