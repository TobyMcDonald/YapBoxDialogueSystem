using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class DialoguePlayer : MonoBehaviour
{
    private TextMeshProUGUI m_nameText;
    private TextMeshProUGUI m_dialogueText;

    [SerializeField] private TMP_FontAsset m_font;

    [SerializeField] private Sprite m_dialogueBoxBG;
    [SerializeField] private Sprite m_characterImage;

    [SerializeField] private bool m_showCharacter;

    [Multiline]
    [SerializeField] private DialogueText[] m_dialogue;
    //[SerializeField] private string[] m_text;

    [SerializeField] private int m_dialogueIndex;

    [SerializeField] private float m_charDelayTime;

    [SerializeField] private GameObject m_dialogueBox;

    private DialogueBoxReferences m_dialogueBoxReferences;

    // Start is called before the first frame update
    void Start()
    {
        m_dialogueBoxReferences = m_dialogueBox.GetComponent<DialogueBoxReferences>();
        m_dialogueBoxReferences.boxBGSprite.sprite = m_dialogueBoxBG;
        m_dialogueBoxReferences.characterProfile.sprite = m_characterImage;
        m_nameText = m_dialogueBoxReferences.nameText;
        m_dialogueText = m_dialogueBoxReferences.dialogueText;
        m_dialogueText.font = m_font;
        m_dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateDialogueBox();            
            m_dialogueIndex++;
        }
    }

    private void CreateDialogueBox()
    {
        DialogueBoxReferences dbCheck = FindObjectOfType<DialogueBoxReferences>();
        if (!dbCheck)
        {
            GameObject m_db = Instantiate(m_dialogueBox);
            StartCoroutine(PlayText(m_dialogueIndex, m_db.GetComponent<DialogueBoxReferences>()));
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_db.transform);
            if (!m_showCharacter)
                m_db.GetComponent<DialogueBoxReferences>().characterProfile.enabled = false;
        }
        else
        {
            StartCoroutine(PlayText(m_dialogueIndex, dbCheck));
            if (!m_showCharacter)
                dbCheck.characterProfile.enabled = false;
        }
    }

    private IEnumerator PlayText(int index, DialogueBoxReferences db)
    {
        db.dialogueText.text = "";
        //foreach (char chara in m_dialogue[index])
        //{
        //    db.dialogueText.text += chara;
        //    yield return new WaitForSeconds(m_charDelayTime);
        //}
        yield return null;
    }
}
