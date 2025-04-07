using UnityEngine;
using UnityEngine.UI;

public class MuteVolumen : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private Image buttonImage;
    private bool isMuted = false;
    private float previousVolume = 1f;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = soundOnSprite;
    }

    public void ToggleMute()
    {
        if (MusicManager.Instance == null) return;

        isMuted = !isMuted;

        if (isMuted)
        {
            previousVolume = MusicManager.Instance.GetVolume();
            MusicManager.Instance.SetVolume(0f);
            buttonImage.sprite = soundOffSprite;
        }
        else
        {
            MusicManager.Instance.SetVolume(previousVolume);
            buttonImage.sprite = soundOnSprite;
        }
    }
}

