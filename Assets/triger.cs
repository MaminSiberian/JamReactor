using UnityEngine;

public class triger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameDirector.PlayNextRoomEvent();
        }
    }
}
