using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class DialogueTrigger : MonoBehaviour
{
    // visual cue appears when player in collider radius 
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header(" Engage in Conversation Button")]
    [SerializeField] private Button engageConvo; 
    private bool playerInRange;
    private bool playerReading; 

    private KeyCode interactKey = KeyCode.I; 

    private void Awake()
    {
        // for visual cue + trigger ote 
        playerInRange = false;
        // for continuing without on trigger enter 
        playerReading = false; 

        visualCue.SetActive(false); 
    }

    private void Update()
    {
        ShowVisualCue();
        engageConvo.onClick.AddListener(HasPlayerClicked); 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
         if(collider.gameObject.tag == "Player")
        {

            playerInRange = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange= false;
        }
    }

    // if player clicks continue reading button , the bool is set to true , works like OTE for narrative reading 
    private void HasPlayerClicked()
    {
        playerReading = true;
        engageConvo.onClick.RemoveListener(HasPlayerClicked);
    }


    /// <summary>
    ///  error might spawn until STOPNPC implemented 
    /// </summary>
    private void ShowVisualCue()
    {
        // does not allow player to interact and throw off dialogue loop after press, allowing only submit by extension
        if(playerInRange &&  !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            // exhibit a 


        if(/*Interactor.GetInstance().isInteractPressed &&*/ Input.GetKeyDown(interactKey))
     
                {
                SoundManager.GetInstance().PlaySingleSounds("InteractSound");
                Debug.Log("Sound : Interact "); 
                //   Debug.Log(inkJSON.text);  refactored to no longer include debug and instead reference the DM 
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                 }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }


    private void ShowNarrativeCue()
    {

    }
   
}
