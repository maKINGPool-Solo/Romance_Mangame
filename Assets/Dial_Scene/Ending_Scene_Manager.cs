using TMPro;
using UnityEngine;

public class Ending_Scene_Manager : MonoBehaviour
{

    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_what;
    public SpriteRenderer back;

    public void MakeText(string name, string text)
    {
        text_name.text = name;
        text_what.text = text;
    }

    public void MakeBack(Sprite back)
    {
        this.back.sprite = back;
    }
}
