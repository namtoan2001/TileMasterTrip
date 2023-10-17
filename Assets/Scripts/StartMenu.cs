using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource clickBtn;

    public void Start()
    {
        if (clickBtn == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }
    }

    public void StartGame()
    {
        if (clickBtn.isPlaying)
        {
            return;
        }

        clickBtn.Play();

        StartCoroutine(LoadNextSceneAfterAudio());
    }

    private IEnumerator LoadNextSceneAfterAudio()
    {
        while (clickBtn.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
