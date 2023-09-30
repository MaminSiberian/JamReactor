using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private RoomEvent _eventToHappen;
    [SerializeField] private bool _oculusOnThePlayer;
    [SerializeField] private bool _vaseIsBroken;
    [SerializeField] private bool _catIsGone;
    [SerializeField] private bool _TVIsBroken;
    [SerializeField] private bool _windowIsBroken;
    [SerializeField] private bool _sofaIsBurning;
    [SerializeField] private bool _roomIsBurning;

    public static GameDirector instance { get; private set; }

    public static bool oculusOnThePlayer;

    public static RoomEvent eventToHappen;
    public static bool vaseIsBroken { get; private set; } // 1
    public static bool catIsGone { get; private set; } // 2
    public static bool TVIsBroken { get; private set; } // 3
    public static bool windowIsBroken { get; private set; } // 4
    public static bool sofaIsBurning { get; private set; } // 5
    public static bool roomIsBurning { get; private set; } // 6

    private static string roomScene = "Room";

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
    private void RestartRoom()
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
        sofaIsBurning = _sofaIsBurning;
        roomIsBurning = _roomIsBurning;
    }
    [Button]
    public static void PlayNextRoomEvent()
    {
        oculusOnThePlayer = true;

        vaseIsBroken = false;
        catIsGone = false;
        TVIsBroken = false;
        windowIsBroken = false;
        sofaIsBurning = false;

        eventToHappen += 1;

        if ((int)eventToHappen > 7)
        {
            Debug.Log("Слишком много возвращений в комнату! Возвращений должно быть 7");
            return;
        }

        vaseIsBroken = (int)eventToHappen > 1;
        catIsGone = (int)eventToHappen > 2;
        TVIsBroken = (int)eventToHappen > 3;
        windowIsBroken = (int)eventToHappen > 4;
        sofaIsBurning = (int)eventToHappen > 5;
        roomIsBurning = (int)eventToHappen > 6;

        SceneManager.LoadScene(roomScene);
    }
    public static void PlayGame()
    {
        PlayNextRoomEvent(); // ПОМЕНЯТЬ ПОТОМ
    }
}
