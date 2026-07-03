using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

// 게임 진행 상태
public struct GameState
{
    public int char_id;
    public int date;
}

[Serializable]
public struct CharInfo
{
    public int id;
    public string name;
}

[Serializable]
public struct CharDial
{
    public Dial[] dial_info;
}


public class Dial_Manager : MonoBehaviour
{
    [SerializeField]
    public CharDial[] dial_info;

    [SerializeField]
    public CharInfo[] char_info;

    public InputAction next;

    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_what;
    public GameObject panel_button;

    GameState current;
    Dial current_dial;
    int dial_id;

    void Start()
    {
        GameState st = new GameState();
        st.char_id = 0;
        st.date = 0;
        InitDial(st);
    }

    public void InitDial(GameState g)
    {
        current = g;
        current_dial = dial_info[g.char_id].dial_info[g.date];
        dial_id = 0;
        MakeText(0);

        next.Enable();
        next.performed += ctx=>Next();
        panel_button.SetActive(false);
    }

    void MakeText(int id)
    {
        text_name.text = char_info[current_dial.dials[id].talker].name;
        text_what.text = current_dial.dials[id].text;
    }

    void Next()
    {
        dial_id++;
        MakeText(dial_id);
    }
}
