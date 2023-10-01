using UnityEngine;

public class triger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameDirector.PlayNextRoomEvent();
    }
}
