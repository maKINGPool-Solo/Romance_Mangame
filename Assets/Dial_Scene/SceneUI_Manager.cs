using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI_Manager : MonoBehaviour
{
    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_what;
    public GameObject panel_button;
    public Slider slider_like;

    public Dial_Manager dm;

    private void Start()
    {
        // 씬이 시작되면 싱글톤에 자신을 등록
        if (Like_Manager.instance != null)
        {
            Like_Manager.instance.RegisterSceneUI(this);
        }
    }

    private void OnDestroy()
    {
        // 씬이 끝나거나 파괴되면 싱글톤에서 참조 해제
        if (Like_Manager.instance != null && Like_Manager.instance.SceneUI == this)
        {
            Like_Manager.instance.UnregisterSceneUI();
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