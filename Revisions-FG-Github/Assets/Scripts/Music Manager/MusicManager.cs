using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [SerializeField] private AudioClip introMusic;        
    [SerializeField] private AudioClip levelMenuMusic;   
    [SerializeField] private AudioClip[] levelMusic;      

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;  
            audioSource.volume = 0.4f;
            PlayMusicForCurrentScene(); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();  
    }

    private void PlayMusicForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Intro UI" && !audioSource.isPlaying)
        {
            PlayMusic(introMusic);  
        }
        else if (sceneName == "Level Menu" && !audioSource.isPlaying)
        {
            PlayMusic(levelMenuMusic);  
        }
        else if (sceneName.StartsWith("Level"))  
        {
            int levelIndex = GetLevelIndex(sceneName);
            if (levelIndex >= 0 && levelIndex < levelMusic.Length) 
            {
                if (!audioSource.isPlaying || audioSource.clip != levelMusic[levelIndex])
                {
                    PlayMusic(levelMusic[levelIndex]); 
                }
            }
        }
        else if (sceneName == "LoadingScene")
        {
            StopMusic(); 
        }
    }

    private void PlayMusic(AudioClip music)
    {
        if (music != null && audioSource.clip != music)
        {
            audioSource.clip = music;
            audioSource.Play();
        }
    }

    private void StopMusic()
    {
        audioSource.Stop(); 
    }

    private int GetLevelIndex(string sceneName)
    {
        int levelNumber;
        if (int.TryParse(sceneName.Replace("Level ", ""), out levelNumber))
        {
            return levelNumber - 1;  
        }
        return -1;  
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }
}
