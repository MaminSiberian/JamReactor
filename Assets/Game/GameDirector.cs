using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private RoomEvent _eventToHappen;
    [SerializeField] private bool _oculusOnThePlayer;
    [SerializeField] private bool _vaseIsBroken;
    [SerializeField] private bool _catIsGone;
    [SerializeField] private bool _TVIsBroken;
    [SerializeField] private bool _windowIsBroken;

    public static GameDirector instance { get; private set; }

    public static bool oculusOnThePlayer;

    public static RoomEvent eventToHappen;
    public static bool vaseIsBroken;
    public static bool catIsGone;
    public static bool TVIsBroken;
    public static bool windowIsBroken;

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
    public void BackToRoom()
    {
        SceneManager.LoadScene(roomScene);
    }
    [Button]
    private void SetRoomConfig()
    {
        eventToHappen = _eventToHappen;
        oculusOnThePlayer = _oculusOnThePlayer;
        vaseIsBroken = _vaseIsBroken;
        catIsGone = _catIsGone;
        TVIsBroken = _TVIsBroken;
        windowIsBroken = _windowIsBroken;
    }
}
