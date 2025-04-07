using UnityEngine;

public class AutoMusicLoader : MonoBehaviour
{
    public GameObject musicManagerPrefab;

    void Awake()
    {
        // Si no encontramos un MusicManager en la escena, creamos uno nuevo
        if (FindObjectOfType<MusicManager>() == null)
        {
            Instantiate(musicManagerPrefab);
        }
    }
}
