using System;
using TMPro;
using UnityEngine;

public class EventText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static event Action OnAnimEndedEvent;

    private static Animator anim;
    private static TextMeshProUGUI text;
    private static string eventAnim = "Event";

    private const string startLine = "Вот ты и дома! Время поиграть в окулус";
    private const string endLine = "Оно того стоило! Пора сваливать...";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        text = _text;
    }
    private void Start()
    {
        if (GameDirector.eventToHappen == RoomEvent.None)
            PlayEventAnim(startLine);

        if (GameDirector.eventToHappen == RoomEvent.Final && GameDirector.roomIsBurning)
            PlayEventAnim(endLine);
    }
    public static void PlayEventAnim(string eventText)
    {
        text.text = eventText;
        anim.Play(eventAnim);
    }
    public void OnAnimEnded()
    {
        OnAnimEndedEvent?.Invoke();
    }

}
