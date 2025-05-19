using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;

    private AudioSource audioSource;
    private string currentScene = "";

    void Awake()
    {
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        float guardarVolumen = PlayerPrefs.GetFloat("gameVolume", 0.5f);
        SetVolume(guardarVolumen);
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        PlayMusicForScene(currentScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentScene)
        {
            currentScene = scene.name;
            PlayMusicForScene(currentScene);
        }
    }

    void PlayMusicForScene(string sceneName)
    {
        StopAllCoroutines(); // Siempre paramos cualquier bucle

        if (IsMenuScene(sceneName))
        {
            if (audioSource.clip != menuMusic)
            {
                StartCoroutine(PlayMenuMusicWithDelay());
            }
        }
        else if (sceneName == "Nivel1")
        {
            if (audioSource.clip != level1Music || !audioSource.isPlaying)
            {
                PlayLoopMusic(level1Music);
            }
        }
        else if (sceneName == "Nivel2")
        {
            if (audioSource.clip != level2Music || !audioSource.isPlaying)
            {
                PlayLoopMusic(level2Music);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }


    bool IsMenuScene(string sceneName)
    {
        return sceneName == "Menú" ||
               sceneName == "Opciones" ||
               sceneName == "Enemigos" ||
               sceneName == "Tutorial" ||
               sceneName == "Jugar";
    }

    IEnumerator PlayMenuMusicWithDelay()
    {
        audioSource.clip = menuMusic;
        audioSource.loop = false;

        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(menuMusic.length + 3f);
        }
    }

    void PlayLoopMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void PlayOneShotMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("gameVolume", value);
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
