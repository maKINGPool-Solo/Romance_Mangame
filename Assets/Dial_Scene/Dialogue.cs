using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public int talker;
    public int listener;
    public int face;
    [TextArea(1, 3)]
    public string text;
}

[Serializable]
public class Choice
{
    [TextArea(1, 3)]
    public string text;

    [SerializeField]
    public Dialogue[] good;

    [SerializeField]
    public Dialogue[] bad;
}
