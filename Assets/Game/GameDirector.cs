using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance { get; private set; }

    public static bool oculusOnThePlayer;

    public static RoomEventsEnum eventToHappen;
    public static bool TVIsBroken;

    private string roomScene = "Room";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    [Button]
    public void BackToRoom(RoomEventsEnum eventToHappen = RoomEventsEnum.None)
    {
        SceneManager.LoadScene(roomScene);
    }
}
