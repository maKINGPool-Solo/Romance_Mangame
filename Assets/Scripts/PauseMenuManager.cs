using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pausePanel.activeSelf)
            {
                return;
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);
        TimeManager.Instance.OpenPauseMenu();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        TimeManager.Instance.ClosePauseMenu();
    }

    public void GoToTitle()
    {
        Destroy(TimeManager.Instance.gameObject);
        Destroy(GameObject.Find("Time_UI"));

        if (Like_Manager.instance != null)
        {
            Destroy(Like_Manager.instance.gameObject);
            Like_Manager.instance = null;
        }

        CharacterProgress.ClearAll();

        SceneManager.LoadScene("TitleScene");
    }
}
