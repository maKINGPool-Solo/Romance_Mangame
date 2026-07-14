using UnityEngine;

public class BGM_Player : MonoBehaviour
{
    public AudioSource bgm_player;


    void OnEnable()
    {
        TimeManager.OnDayChanged += ResetBGM;
        bgm_player.time = BGM.mark;
        bgm_player.Play();
        Debug.Log(bgm_player.time);
    }

    void OnDisable()
    {
        BGM.mark = bgm_player.time;

        TimeManager.OnDayChanged -= ResetBGM;

        Debug.Log(bgm_player.time);
    }

    void ResetBGM()
    {
        BGM.mark = 0f;
        bgm_player.time = BGM.mark;
        bgm_player.Play();
        Debug.Log(bgm_player.time);
    }
}
