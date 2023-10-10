using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// tt:  singleton interactor implements interface IInteract, allowing player to press I to engage in interactions using the input system. 
/// </summary>
public  class Interactor : MonoBehaviour, IInteract
{
    private static Interactor instance;
    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); return; }
        instance = this;
        isInteractPressed = false; 
    }
    public static Interactor GetInstance()
    {
        return instance;
    }
    [Header("Interactor Configurations")]
    [SerializeField] private GameObject player;
    [SerializeField] private bool isInteractPressed;
    [SerializeField] private bool isTalkPressed;
    [SerializeField] private bool isSubmitPressed; 


    private KeyCode interactKey = KeyCode.I;
    // not ideal, would prefer to reference on image click 
    private KeyCode talkKey = KeyCode.T;

    private KeyCode submitKey = KeyCode.Return;


 
    private void Update()
    {
        GetInteractPressed();
        GetTalkPressed();
        GetSubmitPressed(); 
    }
/// <summary>
/// THIS WORKS BUT SHOULD RETURN FALSE OTHERWISE INSTEAD OF GKU BECAUSE
/// OF TIED FUNCTIONALITIES TO DIALOGUE TRIGGER
/// </summary>
    public void GetInteractPressed()
    {
        if (Input.GetKeyDown(interactKey))
        {
           isInteractPressed = true;
            Debug.Log("Interaction engaged"); 
        }
        if(Input.GetKeyUp(interactKey))
        {
            isInteractPressed = false; 
        }
        // return?
    }

    public void GetTalkPressed()
    {
        if (Input.GetKeyDown(talkKey))
        {
            isTalkPressed = true;
            Debug.Log("Talk engaged");
        }
        if (Input.GetKeyUp(talkKey))
        {
            isTalkPressed = false;
        }
        // return?
    }

    public void GetSubmitPressed()
    {
        if (Input.GetKeyDown(submitKey))
        {
            isSubmitPressed = true;
            Debug.Log("Submit engaged");
        }
        if (Input.GetKeyUp(submitKey))
        {
            isSubmitPressed = false;
        }
        // return?
    }
}
