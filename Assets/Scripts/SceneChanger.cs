using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneChanger : MonoBehaviour
{
    public AudioSource audioSource;
    
    public void ChangeScene(string sceneName)
    {
        audioSource.Play();
        SceneManager.LoadScene(sceneName: sceneName);
    }
}