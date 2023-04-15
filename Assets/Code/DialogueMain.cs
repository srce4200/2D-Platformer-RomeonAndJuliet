using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMain : MonoBehaviour
{
    [SerializeField] GameObject dialogHolder_1;
    [SerializeField] Text nameText_1;
    [SerializeField] Text convoText_1;
    [Space]
    [SerializeField] GameObject dialogHolder_2;
    [SerializeField] Text nameText_2;
    [SerializeField] Text convoText_2;

    [Header("Ne tikat tm odspodi")]
    public List<DialogText> dialogTexts = new List<DialogText>();
    public List<string> peopleNames = new List<string>();
    string currentText = "";
    int currentDialogNumber = 0;
    bool canSkip = false;
    DialogueList dialogueList; 
    bool oneTime1;
    private void Start()
    {
        dialogTexts = null;
        peopleNames = null;
    }
    public void StartConversation(DialogueList dialog, bool oneTime)
    {
        dialogueList = dialog;
        oneTime1 = oneTime;
        dialogTexts = dialog.DialogConvo;
        peopleNames = dialog.peopleInConvo;
        StartCoroutine("TypeText");
        GetComponentInChildren<PlayerMovement>().enabled = false;
        canSkip = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canSkip && dialogTexts != null)
        {
            if(currentText.Length == dialogTexts[currentDialogNumber].text.Length)
            {
                convoText_1.text = ""; convoText_2.text = ""; //resetiraj tekst
                currentDialogNumber++; //naslednji dialog
                
                if (currentDialogNumber >= dialogTexts.Count) //konec pogovora
                {
                    canSkip = false;
                    StartCoroutine(DelayEnd());
                    return;
                }
                else
                {
                    StartCoroutine("TypeText");
                }                
            }
            else
            {
                StopCoroutine("TypeText");
                currentText = dialogTexts[currentDialogNumber].text;
                DialogHolderSelector();
            }
        }
    }

    IEnumerator TypeText()
    {
        for(int i = 0; i < dialogTexts[currentDialogNumber].text.Length; i++)
        {        
            currentText = dialogTexts[currentDialogNumber].text.Substring(0, i + 1);

            DialogHolderSelector();
            yield return new WaitForSeconds(0.1f);
        }
    }
    void DialogHolderSelector()
    {
        if (dialogTexts[currentDialogNumber].characterIndex %2 == 0)
        {
            dialogHolder_1.SetActive(true);
            dialogHolder_2.SetActive(false);

            nameText_1.text = peopleNames[dialogTexts[currentDialogNumber].characterIndex];
            convoText_1.text = currentText;
        }
        else if(dialogTexts[currentDialogNumber].characterIndex % 2 == 1)
        {
            dialogHolder_1.SetActive(false);
            dialogHolder_2.SetActive(true);

            nameText_2.text = peopleNames[dialogTexts[currentDialogNumber].characterIndex];
            convoText_2.text = currentText;
        }
    }    
    IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(0.01f);
        dialogHolder_1.SetActive(false); dialogHolder_2.SetActive(false);
        convoText_1.text = ""; convoText_2.text = "";
        currentDialogNumber = 0;
        dialogTexts = null; peopleNames = null;
        if (oneTime1)
        {
            dialogueList.gameObject.tag = "Untagged";
            Destroy(dialogueList);
        }
        GetComponentInChildren<PlayerMovement>().enabled = true;
    }
}
