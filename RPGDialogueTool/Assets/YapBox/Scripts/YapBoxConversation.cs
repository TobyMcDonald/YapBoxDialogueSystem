using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YapBox
{

    [CreateAssetMenu(fileName = "NewConversation", menuName = "YapBox/ConversationScriptableObject", order = 1)]
    public class YapBoxConversation : ScriptableObject
    {
        public List<DialogueText> dialogue;

        public int conversationPosition;
    }
}