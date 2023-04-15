using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public string interactTag;
    public GameObject interactKeyDisplay;

    GameObject currentInteract;
    DialogueMain dialogueMain;

    private void Start()
    {
        dialogueMain = GetComponentInParent<DialogueMain>();
    }
    private void Update()
    {
        if(currentInteract != null && dialogueMain.dialogTexts == null && currentInteract.GetComponent<DialogueList>().startDialogOnTrigger)
        {
            currentInteract.GetComponent<DialogueList>().StartConversation();
            print(currentInteract.name);
            currentInteract = null;
        }
        else if(Input.GetKeyDown(KeyCode.E) && currentInteract != null && dialogueMain.dialogTexts == null)
        {
            currentInteract.GetComponent<DialogueList>().StartConversation();
            print(currentInteract.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == interactTag)
        {
            currentInteract = collision.gameObject;

            interactKeyDisplay.SetActive(true);
            interactKeyDisplay.transform.position = new Vector2(currentInteract.transform.position.x, currentInteract.transform.position.y + 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == interactTag)
        {
            currentInteract = null;

            interactKeyDisplay.SetActive(false);
        }
    }
}
