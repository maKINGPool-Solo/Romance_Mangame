using System;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.LightTransport;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ending_Dial_Manager : MonoBehaviour
{
    [SerializeField]
    public Dial[] dial_info;

    [SerializeField]
    public CharInfo[] char_info;

    public Sprite[] back_imgs;

    [Header("--- 엔딩별 BGM 설정 ---")]
    [Tooltip("0번부터 10번 엔딩까지 순서대로 배경음악을 인펙터에서 넣어주세요.")]
    public AudioClip[] ending_bgms; 
    private AudioSource bgmPlayer;  

    public InputAction next;

    public Ending_Scene_Manager SceneUI;
    Dial current_dial;
    int dial_id;

    void Start()
    {
        
        bgmPlayer = GetComponent<AudioSource>();
        if (bgmPlayer == null)
        {
            bgmPlayer = gameObject.AddComponent<AudioSource>();
        }
        
       
        bgmPlayer.loop = true;
        bgmPlayer.playOnAwake = false;

        StartCoroutine(WaitForFade());

        int ending = EndingData.ResultEndingId;
        InitDial(ending);
    }

    // 화면 Fade In 될 때까지 기다리기
    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(1.0f);

        ConnectInputAction(true);
    }

    public void InitDial(int id)
    {
        current_dial = dial_info[id];
        dial_id = 0;

        MakeText(0);
        MakeBack(id);
        PlayEndingBGM(id); 
    }

    // [추가] 엔딩 ID에 맞는 배경음악을 틀어주는 함수
    void PlayEndingBGM(int id)
    {
        // 배열에 음악이 잘 들어있고, 현재 엔딩 번호에 맞는 음악이 있다면 재생
        if (ending_bgms != null && id < ending_bgms.Length && ending_bgms[id] != null)
        {
            bgmPlayer.clip = ending_bgms[id];
            bgmPlayer.Play();
        }
    }

    void MakeText(int id)
    {
        if (current_dial.dials.Length <= id)
        {
            GoToTitle();
            return;
        }

        string name;
        string text;
        if (current_dial.dials[id].talker == -1) name = "나";
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

    void GoToTitle()
    {
        ConnectInputAction(false);

        if (Like_Manager.instance != null)
        {
            Destroy(Like_Manager.instance.gameObject);
            Like_Manager.instance = null;
        }

        CharacterProgress.ClearAll();

        SceneManager.LoadScene("TitleScene");
    }

    void ConnectInputAction(bool isEnable)
    {
        if (isEnable)
        {
            next.performed += ctx => Next();
            next.Enable();
        }
        else
        {
            next.performed -= ctx => Next();
            next.Disable();
        }
    }
}
