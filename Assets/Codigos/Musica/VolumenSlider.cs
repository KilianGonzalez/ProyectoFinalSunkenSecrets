using UnityEngine;
using UnityEngine.UI;

public class VolumenSlider : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        // Esperamos un poco por si el MusicManager tarda en inicializarse
        Invoke(nameof(SetupSlider), 0.1f);
    }

    void SetupSlider()
    {
        if (MusicManager.Instance != null)
        {
            slider.value = MusicManager.Instance.GetVolume();
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetVolume(value);
        }
    }
}