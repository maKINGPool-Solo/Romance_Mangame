using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TitleFadeManager titleFadeManager;

    public void StartGame()
    {
        titleFadeManager.FadeToMainScene("MainScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
