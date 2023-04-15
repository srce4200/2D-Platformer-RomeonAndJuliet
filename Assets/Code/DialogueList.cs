using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogText
{
        public int characterIndex;
        public string text;
}
public class DialogueList : MonoBehaviour
{
    public bool startDialogOnTrigger;
    DialogueMain convoUi;
    public List<string> peopleInConvo = new List<string>();
    public List<DialogText> DialogConvo = new List<DialogText>();

    private void Awake()
    {
        convoUi = FindAnyObjectByType<DialogueMain>();
    }
    public void StartConversation()
    {
        convoUi.StartConversation(this, startDialogOnTrigger);
    }
}
