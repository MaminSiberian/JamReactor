using UnityEngine;
using UnityEngine.Events;

public class EventManagers : MonoBehaviour
{
    public static UnityEvent smoothOn = new UnityEvent();
    public static UnityEvent smoothOff = new UnityEvent();

    public static void Hide()
    {
        smoothOff?.Invoke();
    }
    public static void SmoothOn()
    {
        smoothOn?.Invoke();
    }
}
