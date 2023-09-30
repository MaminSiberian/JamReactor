using UnityEngine;
using UnityEngine.Events;

public static class MyEventManager
{
    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnPause = new UnityEvent();
    public static UnityEvent OnIncreasePoints = new UnityEvent();
    public static UnityEvent OnCatchSomthing = new UnityEvent();
    public static UnityEvent<float, float> OnCameraShake  = new UnityEvent<float, float>();
    // public static UnityEvent  OnShakeCamera = new UnityEvent<float,float>();
    public static UnityEvent <int> test = new UnityEvent<int>();

    public static void CatchSomthing()
    {
        OnCatchSomthing.Invoke();       
    }

    public static void CameraShake(float intensityShakeCamera, float timeShakeCamera)
    {
        OnCameraShake.Invoke(intensityShakeCamera, timeShakeCamera);
    }

    public static void SendPause()
    {
        OnPause.Invoke();
    }
    public static void SendStartGame()
    {
        OnStartGame.Invoke();
    }

    public static void SendGameOver()
    {
        OnGameOver.Invoke();
    }

    public static void SendIncreasePoints()
    {
        OnIncreasePoints.Invoke();
    }
}