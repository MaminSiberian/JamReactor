using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Room.Player>() && GameDirector.eventToHappen == RoomEvent.Final)
        {
            endScreen.SetActive(true);
        }
    }
}
