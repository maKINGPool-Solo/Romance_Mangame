using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MainMonologueManager : MonoBehaviour
{
    public GameObject monologuePanel;
    public TextMeshProUGUI monologueText;
    public static bool IsMonologueActive = false;

    [TextArea]
    public string[] lines;

    private int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        monologuePanel.SetActive(true);
        monologueText.text = lines[currentIndex];
        IsMonologueActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AdvanceLine();
        }
    }

    void AdvanceLine()
    {
        currentIndex++;
        if(currentIndex >= lines.Length)
        {
            EndMonologue();
        }
        else
        {
            monologueText.text = lines[currentIndex];
        }
    }

    void EndMonologue()
    {
        monologuePanel.SetActive(false);
        TimeManager.Instance.isPaused = false;
        IsMonologueActive = false;
    }
}
