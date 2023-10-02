using UnityEngine;

public class SmoothChangeScene : MonoBehaviour
{
    
    public Animator _anim;
    private void Start()
    {
        EventManagers.smoothOn.AddListener(ChangeOn);
        EventManagers.smoothOff.AddListener(Hide);
        Hide();
    }
    public void ChangeOn()
    {
        _anim.CrossFade("On", 0f);
    }
    public void Hide()
    {
        _anim.CrossFade("Hide", 0f);
    }



}
