using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public int talker;
    public int face;
    public string text;
}

[Serializable]
public class Choice
{
    public string text;
}

[Serializable]
public class Reaction
{
    public bool isGood;
    public string text;
}