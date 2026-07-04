using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LightTransport;
using UnityEngine.SceneManagement;

public class Ending_Dial_Manager : MonoBehaviour
{
    [SerializeField]
    public Dial[] dial_info;

    [SerializeField]
    public CharInfo[] char_info;

    public Sprite[] back_imgs;

    public InputAction next;

    public Ending_Scene_Manager SceneUI;
    Dial current_dial;
    int dial_id;

    void Start()
    {
        InitDial(0);
    }
    public void InitDial(int id)
    {
        current_dial = dial_info[id];
        dial_id = 0;
        MakeText(0);
        MakeBack(id);

        next.Enable();
        next.performed += ctx => Next();
    }

    void MakeText(int id)
    {
        if (current_dial.dials.Length <= id)
        {
            return;
        }

        string name;
        string text;
        if (current_dial.dials[id].talker == -1) name = "³ª";
        else name = char_info[current_dial.dials[id].talker].name;
        text = current_dial.dials[id].text;
        if (SceneUI != null) SceneUI.MakeText(name, text);
    }
    void MakeBack(int id)
    {
        Sprite back = back_imgs[id];

        if (SceneUI != null) SceneUI.MakeBack(back);
    }

    void Next()
    {
        dial_id++;
        MakeText(dial_id);
    }
}
