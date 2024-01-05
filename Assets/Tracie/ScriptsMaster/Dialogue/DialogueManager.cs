using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems; 
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.UI;
/// <summary>
/// tt : 
/// inkJSON files are sent to the Dialogue Manager which then 
///  creates a story object with ink file  that plays through, 
/// enables choice and displays the dialogue 
/// notes : 
/// a layermask could be used to seperate interactive object prompts and dialogue system 
/// with this implementation, where is the best place to call the load scene? 
/// </summary>
public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); return; }
        instance = this;

        // 
        dialogueVariables = new DialogueVariables(loadGlobalsJSON); 
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON; 


    [Header("Dialogue UI Configurationa")]
    [SerializeField] private GameObject dialoguePanel; 
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Dialogue Choices Configurations")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText; 
    private Story currentStory;
    private float waitFor = .2f;

    //[Header("Narrative Reading Configurations")]
    //[SerializeField] private GameObject continueReadingButton; 

    [Header("Parameters Configurations")]
    [SerializeField] private float typingSpeed = .04f;


    private DialogueVariables dialogueVariables;
    private Coroutine displayLineCoroutine; 
    // readonly
    public bool dialogueIsPlaying { get; private set; }
    private bool canContinueToNextLine = false; 

 
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        GatherAllChoices(); 
    }

    private void Update()
    {

        // return right away if dialogue isnt playing
        if (!dialogueIsPlaying)
        {
            return; 
        }
        // handle continuing to the next line in the dialogue when submit is pressed 
        // exhibit b 
        //double check  prevents the story from continuing if there are choices 
        if (canContinueToNextLine && 
            currentStory.currentChoices.Count == 0
            && Input.GetKeyDown(KeyCode.T))
        {
            SoundManager.GetInstance().PlaySingleSounds("TalkSound");
            Debug.Log("Sound : talk "); 
            ContinueStory(); 
        }

    }
    /// <summary>
    /// tt: 
    /// </summary>
    /// <param name="inkJSON"></param>

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        // add listener
        dialogueVariables.StartListening(currentStory); 

        ContinueStory(); 
    }

    /// <summary>
    ///  empty ink json file passed in 
    /// </summary>
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(waitFor);
        // remove listener
        dialogueVariables.StopListening(currentStory); 
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = ""; 
    }

    /// <summary>
    ///  
    /// </summary>
    private void ContinueStory()
    {
        // always returns false if there are choices but havent been selected 
        if (currentStory.canContinue)
        {
            //guard clause to set text for the current dialogue line 
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine); 
            }
            SoundManager.GetInstance().PlaySingleSounds("Chatting"); 
            Debug.Log("Sound : Chatting"); 
           displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue())); 
            // CHANGING TO IEN 4 TYPING EFFECT 
            // somewhat like popping a line out of a stack, settting the text for the current dialogue line
            //dialogueText.text = currentStory.Continue();
            // CHANGING TO IEN  4 TYPING EFFECT 
       
        }
        else
        {

            StartCoroutine(ExitDialogueMode()); 
        }
    }

    /// <summary>
    /// tt :   initializes array of cT to be the same as out choices, and for each choice in the arrray we will initialize 
    /// the corresponding text using an index for the choice to ensure matching 
    /// </summary>
    private void GatherAllChoices()
    {
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0; 
        foreach(GameObject choice in choices)
        {
            //text is a child of the choice selection  
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; 
        }
    }

    /// <summary>
    /// tt: 
    /// </summary>
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; 
        //safeguard if there are more choices than the array 
        if(currentChoices.Count > choices.Length)
        {
            Debug.Log("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count); 
        }

        // loop through all choice game objects in UI and display them in correlation with the INK story 
        int index = 0; 
        // enable and initialize the choices up to the amount of choices for this line of dialogue 
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++; 
        }
        // go through the remaining choices the UI supports and make sure theyre hidden 
        for(int i = index; i < choices.Length; i ++)
        {
            choices[i].gameObject.SetActive(false);
        }

        // start SFC 
        StartCoroutine(SelectFirstChoice());
    }

    private void HideChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }


    /// <summary>
    ///  setting the first selected choice using a coroutine 
    ///event system requires we clear first, then wait for at least once frame
   /// before we set the current selected object. 
   /// </summary>
   /// <returns></returns>
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject); 
    }


    private IEnumerator DisplayLine(string line)
    {

        // clear the dialogue text for the next line 
        dialogueText.text = "  ";
        canContinueToNextLine = false;
        HideChoices(); 
        // display each letter one by one by turning our string into a character array 
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); 
        }
        canContinueToNextLine = true;
        // display choices, if any for this dialogue line
        DisplayChoices();
    }

    



    /// <summary>
    /// tt: on click function enabling player to make choices 
    /// </summary>
    /// <param name="choiceIndex"></param>
    public void MakeChoice( int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            SoundManager.GetInstance().PlaySingleSounds("DialogueSelect");
            Debug.Log("Sound : Dialogue select "); 
            // shouldnt need register on submit 
            ContinueStory();
        }
    
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null; 
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null)
        {
            Debug.Log("Ink Variable was found to be null " + variableName); 
        }
        return variableValue; 
    }
}
