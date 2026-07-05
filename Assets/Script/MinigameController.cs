using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    void OnEnable()
    {
        FadeManager.OnScreenDarkened += ForceQuitMinigame;
    }

    // Update is called once per frame
    void OnDisable()
    {
        FadeManager.OnScreenDarkened += ForceQuitMinigame;
    }

    void ForceQuitMinigame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
