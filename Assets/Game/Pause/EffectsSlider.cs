using UnityEngine;
using UnityEngine.UI;
public class EffectsSlider : MonoBehaviour
{
    [SerializeField] private Pause pause;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }
    private void OnDisable()
    {
        slider?.onValueChanged.RemoveListener(OnValueChanged);
    }
    private void OnValueChanged(float value)
    {
        pause.SetEffectsVolume(value);
    }
}
