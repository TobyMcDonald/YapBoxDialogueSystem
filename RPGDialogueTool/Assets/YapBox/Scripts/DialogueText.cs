using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueText
{
    [SerializeField] private string m_charName;
    [Multiline]
    [SerializeField] private string m_text;
    [SerializeField] private Sprite m_character;
    [SerializeField] private Sprite m_bg;
    [SerializeField] private Sprite m_characterNameBG;

    public string charName => m_charName;
    public string text => m_text;
    public Sprite bg => m_bg;
    public Sprite character => m_character;
    public Sprite characterNameBG => m_characterNameBG;
}
