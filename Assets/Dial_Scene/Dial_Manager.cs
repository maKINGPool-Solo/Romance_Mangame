using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// 게임 진행 상태
public struct GameState
{
    public int char_id;
    public int date;

    public GameState(int char_id, int date)
    {
        this.char_id = char_id;
        this.date = date;
    }
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

    //public TextMeshProUGUI text_name;
    //public TextMeshProUGUI text_what;
    //public GameObject panel_button;

    GameState current;
    public Dial current_dial;
    int dial_id;

    public static Dial_Manager instance = null;

    public SceneUI_Manager SceneUI { get; private set; }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GameState st = new GameState();
        st.char_id = 0;
        st.date = 0;
        InitDial(new GameState(0, 0));
    }

    public void InitDial(GameState g)
    {
        current = g;
        current_dial = dial_info[g.char_id].dial_info[g.date];
        dial_id = 0;
        MakeText(0);

        next.Enable();
        next.performed += ctx=>Next();
        if (SceneUI != null) SceneUI.SetPannelButton(false);
    }

    void MakeText(int id)
    {
        string name = char_info[current_dial.dials[id].talker].name;
        string text = current_dial.dials[id].text;
        if(SceneUI!= null) SceneUI.MakeText(name, text);
    }

    void Next()
    {
        dial_id++;
        MakeText(dial_id);

        //Like_Manager.instance.SetLike(0, 10);
    }

    public void RegisterSceneUI(SceneUI_Manager uiManager)
    {
        SceneUI = uiManager;
    }

    public void UnregisterSceneUI()
    {
        SceneUI = null;
    }
}
