using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using System;


namespace YapBox
{
    public class DialoguePlayer : MonoBehaviour
    {
        private TextMeshProUGUI m_nameText;
        private TextMeshProUGUI m_dialogueText;

        [SerializeField] private TMP_FontAsset m_font;

        [SerializeField] private Sprite m_dialogueBoxBG;
        [SerializeField] private Sprite m_characterImage;

        [SerializeField] private bool m_showCharacter;

        [SerializeField]
        private List<YapBoxConversation> m_conversationCollection;
        public List<YapBoxConversation> conversationCollection => m_conversationCollection;

        [SerializeField] private float m_charDelayTime;

        [SerializeField] private GameObject m_dialogueBox;

        private DialogueBoxReferences m_dialogueBoxReferences;

        private bool m_isPlaying;

        private Coroutine m_playingText;

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
            // Calling through SpcaeBar here for testing
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayConversation(0);
            }
        }


        /// <summary>
        /// Play a conversation from the list stored in the DialoguePlayer ConversationList by their index in the list
        /// </summary>
        /// <param name="conversationIndex"></param>
        public void PlayConversation(int conversationIndex)
        {
            CreateDialogueBox(conversationIndex);
        }

        private void CreateDialogueBox(int conversationIndex)
        {
            DialogueBoxReferences dbCheck = FindObjectOfType<DialogueBoxReferences>();
            if (!dbCheck)
            {
                if (!m_isPlaying)
                {
                    m_isPlaying = true;
                    GameObject m_db = Instantiate(m_dialogueBox);
                    m_playingText = StartCoroutine(PlayText(conversationIndex, m_db.GetComponent<DialogueBoxReferences>()));
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_db.transform);
                    if (!m_showCharacter)
                        m_db.GetComponent<DialogueBoxReferences>().characterProfile.enabled = false;
                }
                else 
                {
                    if (m_playingText != null)
                    {
                        GameObject m_db = Instantiate(m_dialogueBox);
                        StopCoroutine(m_playingText);
                        InstantFinishText(conversationIndex, m_db.GetComponent<DialogueBoxReferences>());
                    }

                }
            }
            else
            {
                if (!m_isPlaying)
                {
                    if (m_conversationCollection[conversationIndex].conversationPosition > m_conversationCollection[conversationIndex].dialogue.Count - 1)
                    {
                        Destroy(dbCheck.gameObject);                 
                        m_playingText = null;
                        m_conversationCollection[conversationIndex].conversationPosition = m_conversationCollection[conversationIndex].dialogue.Count - 1;
                        return;
                    }

                    m_isPlaying = true;
                    m_playingText = StartCoroutine(PlayText(conversationIndex, dbCheck));
                    if (!m_showCharacter)
                        dbCheck.characterProfile.enabled = false;
                }
                else
                {
                    if (m_playingText != null)
                    {
                        StopCoroutine(m_playingText);
                        InstantFinishText(conversationIndex, dbCheck);
                    }
                }
            }
        }

        private void InstantFinishText(int index, DialogueBoxReferences db)
        {
            db.dialogueText.text = "";
            int conversationPos = m_conversationCollection[index].conversationPosition;
            db.nameText.text = m_conversationCollection[index].dialogue[conversationPos].charName;
            db.dialogueText.text += m_conversationCollection[index].dialogue[conversationPos].text;
            db.characterProfile.sprite = m_conversationCollection[index].dialogue[conversationPos].character;
            db.boxBGSprite.sprite = m_conversationCollection[index].dialogue[conversationPos].bg;
            db.nameBGSprite.sprite = m_conversationCollection[index].dialogue[conversationPos].characterNameBG;
            m_conversationCollection[index].conversationPosition++;
            m_isPlaying = false;
        }

        private IEnumerator PlayText(int index, DialogueBoxReferences db)
        {
            db.dialogueText.text = "";
            int conversationPos = m_conversationCollection[index].conversationPosition;

            foreach (char chara in m_conversationCollection[index].dialogue[conversationPos].text)
            {
                db.nameText.text = m_conversationCollection[index].dialogue[conversationPos].charName;
                db.dialogueText.text += chara;
                db.characterProfile.sprite = m_conversationCollection[index].dialogue[conversationPos].character;
                db.boxBGSprite.sprite = m_conversationCollection[index].dialogue[conversationPos].bg;
                db.nameBGSprite.sprite = m_conversationCollection[index].dialogue[conversationPos].characterNameBG;
                yield return new WaitForSeconds(m_charDelayTime);
            }
            yield return null;
            m_conversationCollection[index].conversationPosition++;
            m_isPlaying = false;
        }

        private void OnDisable()
        {
            foreach (var item in m_conversationCollection)
            {
                item.conversationPosition = 0;
            }
        }
    }
}