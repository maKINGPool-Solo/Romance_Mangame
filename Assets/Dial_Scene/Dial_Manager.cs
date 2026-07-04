using JetBrains.Annotations;
using System;
using System.Collections;
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
    public int back_id;
    public int date;

    public GameState(string char_id, string back_id, int date)
    {
        switch (char_id)
        {
            case "A":
                this.char_id = 0;
                this.back_id = 0;
                break;
            case "B":
                this.char_id = 1;
                this.back_id = 1;
                break;
            case "C":
                this.char_id= 2;
                this.back_id= 2;
                break;
            default:
                this.char_id = 0;
                this.back_id = 0;
                break;
        }
        this.date = date-1;
    }
}

[Serializable]
public struct CharInfo
{
    public int id;
    public string name;
    public Sprite[] imgs;
}

[Serializable]
public struct BackInfo
{
    public string name;
    public Sprite img;
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

    [SerializeField]
    public BackInfo[] back_info;

    [SerializeField] 
    private string[] targetScenes;

    [SerializeField]
    public Dialogue[] after_game;

    public InputAction next;
    public static Dial_Manager instance;

    //public TextMeshProUGUI text_name;
    //public TextMeshProUGUI text_what;
    //public GameObject panel_button;

    GameState current;
    int event_id;
    public Dial current_dial;
    int dial_id;
    int reaction_id;
    Dialogue[] current_reaction;

    public bool isGood;
    public bool isSuccess;
    bool isPlayed;

    public SceneUI_Manager SceneUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        TimeManager.Instance.isPaused = true;
        TimeManager.Instance.SetTimeUIVisible(false);

        current = new GameState(DialogueData.SelectedCharacterId, DialogueData.SelectedCharacterId, TimeManager.Instance.currentDay);

        event_id = UnityEngine.Random.Range(0, dial_info[current.char_id].dial_info.Length);

        isPlayed = false;
        //isPlayed = true;

        if (!isPlayed) InitDial();
        else AfterMinigame();
        //InitDial(new GameState("B", "B", 0));
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        TimeManager.Instance.isPaused = false;
        TimeManager.Instance.SetTimeUIVisible(true);
    }


    public void InitDial()
    {
        current_dial = dial_info[current.char_id].dial_info[event_id];
        dial_id = 0;
        MakeText(0);
        MakeBack(current.back_id);

        next.Enable();
        next.performed += ctx=>Next();

        if (SceneUI != null) SceneUI.SetPannelButton(false);

        reaction_id = -1;
    }

    void MakeText(int id)
    {
        if (current_dial.dials.Length <= id)
        {
            ShowButtons();
            return;
        }

        string name;
        string text;
        Sprite face;
        if (current_dial.dials[id].talker == -1) name = "나";
        else name = char_info[current_dial.dials[id].talker].name;
        text = current_dial.dials[id].text;
        if (current_dial.dials[id].listener == -1) face = null;
        else face = char_info[current_dial.dials[id].listener].imgs[current_dial.dials[id].face];
        if(SceneUI!= null) SceneUI.MakeText(name, text, face);
    }

    public void InitReaction()
    {
        reaction_id = 0;
        MakeReaction(reaction_id);

        if (SceneUI != null) SceneUI.SetPannelButton(false);
    }

    void MakeReaction(int id)
    {
        if (current_reaction.Length <= id)
        {
            GotoMinigame();
            return;
        }

        string name;
        string text;
        Sprite face;
        if (current_reaction[id].talker == -1) name = "나";
        else name = char_info[current_reaction[id].talker].name;
        text = current_reaction[id].text;
        if (current_reaction[id].listener == -1) face = null;
        else face = char_info[current_reaction[id].listener].imgs[current_reaction[id].face];
        if (SceneUI != null) SceneUI.MakeText(name, text, face);
    }

    void MakeBack(int id)
    {
        Sprite back = back_info[id].img;

        if (SceneUI != null) SceneUI.MakeBack(back);
    }

    void Next()
    {
        if(reaction_id == -1)
        {
            dial_id++;
            MakeText(dial_id);
        }
        else if(!isPlayed)
        {
            reaction_id++;
            MakeReaction(reaction_id);
        }
        else
        {
            //SceneManager.LoadScene("MainScene");
            FadeManager.Instance.FadeToScene("MainScene", Color.white);
        }

        //Like_Manager.instance.SetLike(0, 10);
    }

    void ShowButtons()
    {
        string[] texts = new string[current_dial.choices.Length];

        for (int i = 0; i < texts.Length; i++) texts[i] = current_dial.choices[i].text;

        if (SceneUI != null) SceneUI.ShowButtons(texts);
    }


    public void ClickButton(int id)
    {
        if (UnityEngine.Random.value <= 0.5f)
        {
            isGood = false;
        }
        else
        {
            isGood = true;
        }

        if (isGood) current_reaction = current_dial.choices[id].good;
        else current_reaction = current_dial.choices[id].bad;

        InitReaction();
    }

    void GotoMinigame()
    {
        if (isGood)
        {
            FadeManager.Instance.FadeToScene("EasyMiniGame", Color.white);
        }
        else
        {
            FadeManager.Instance.FadeToScene("HardMiniGame", Color.white);
        }
    }

    void MakeAfterGame(int id)
    {
        string name;
        string text;
        Sprite face;
        name = char_info[current.char_id].name;
        text = after_game[id].text;
        face = char_info[current.char_id].imgs[after_game[id].face];
        if (SceneUI != null) SceneUI.MakeText(name, text, face);
    }

    void AfterMinigame()
    {
        if (isSuccess)
        {
            if (isGood)
            {
                MakeAfterGame(0);
                Like_Manager.instance.SetLike(current.char_id, 10);
            }
            else
            {
                MakeAfterGame(1);
                Like_Manager.instance.SetLike(current.char_id, 20);
            }
        }
        else
        {
            MakeAfterGame(2);
            Like_Manager.instance.SetLike(current.char_id, -10);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool shouldKeep = false;
        foreach (string sceneName in targetScenes)
        {
            if (scene.name == sceneName)
            {
                if (scene.name == "Dialogue_Scene")
                {
                    AfterMinigame();
                    TimeManager.Instance.isPaused = true;
                    TimeManager.Instance.SetTimeUIVisible(false);
                }
                else
                {
                    TimeManager.Instance.isPaused = false;
                    TimeManager.Instance.SetTimeUIVisible(true);
                }
                shouldKeep = true;
                break;
            }
        }

        if (!shouldKeep)
        {
            Destroy(gameObject);
        }
    }
}
