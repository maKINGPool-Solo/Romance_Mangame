using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct CharLike
{
    public string name;
    public float like;
}
public class Like_Manager : MonoBehaviour
{
    public static Like_Manager instance = null;
    public SceneUI_Manager SceneUI { get; private set; }

    //public Slider slider_like;

    [SerializeField]
    private CharLike[] likes;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Init()
    {
        for(int i=0;i<likes.Length; i++)
        {
            likes[i].like = 0f;
        }
    }

    public void SetLike(int id, float change)
    {
        float a = likes[id].like + change;

        a = Math.Min(a, 100f);
        a = Math.Max(a, 0f);
        likes[id].like = a;

        ShowSlider(id);
    }

    public float GetLike(int id)
    {
        return likes[id].like;
    }

    public void ShowSlider(int id)
    {
        float value = likes[id].like / 100f;
        if(SceneUI!= null) SceneUI.ShowSlider(value);
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
