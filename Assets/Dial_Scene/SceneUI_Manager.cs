using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI_Manager : MonoBehaviour
{
    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_what;
    public GameObject panel_button;
    public Slider slider_like;

    private void Start()
    {
        if (Like_Manager.instance != null)
        {
            Like_Manager.instance.RegisterSceneUI(this);
        }

        if (Dial_Manager.instance != null)
        {
            Dial_Manager.instance.RegisterSceneUI(this);
        }
    }

    private void OnDestroy()
    {
        if (Like_Manager.instance != null &&  Like_Manager.instance.SceneUI == this)
        {
            Like_Manager.instance.UnregisterSceneUI();
        }

        if (Dial_Manager.instance != null)
        {
            Dial_Manager.instance.RegisterSceneUI(this);
        }
    }

    public void MakeText(string name, string text)
    {
        text_name.text = name;
        text_what.text = text;
    }

    public void ShowSlider(float value)
    {
        slider_like.value = value;
    }

    public void SetPannelButton(bool enable)
    {
        panel_button.SetActive(enable);
    }
}