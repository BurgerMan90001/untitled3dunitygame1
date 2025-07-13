using UnityEngine;
using Ink.Runtime;
using System.Collections;


public class DialogueManager : MonoBehaviour
{
    [Header("Dependencies")]
    

    [Header("Dialogue Settings")]
    [SerializeField] private bool _autoPlayDialogue = false;
    [SerializeField] private float _continueDialogueInSeconds = 5f;

    [Header("Ink Story")]
    [SerializeField] private TextAsset _inkJson;

    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private UserInterfaceData _userInterfaceData;

    private Story _story;

    private bool _dialoguePlaying = false;

    private void Awake()
    {
        _story = new Story(_inkJson.text);
    //    _story.variablesState[]
    }
    private void OnEnable()
    {
        // trigger this class' EnterDialogue when the game dialogue event is triggered
        _dialogueData.OnEnterDialogue += EnterDialogue;
    }
    private void OnDisable()
    {
        _dialogueData.OnEnterDialogue -= EnterDialogue;

    }

    private void ContinueDialogue() // called when a continue dialogue button is pressed
    {
        ContinueOrExitStory();

    }
    private void BeginDialogue(string knotName) // happens when beginning a new dialogue with an NPC
    {
        _dialoguePlaying = true;

        _userInterfaceData.ToggleUserInterface(UserInterfaces.Dialogue);

        if (!knotName.Equals(""))
        {
            _story.ChoosePathString(knotName); // jump to the knotname in the ink file
        }
        else
        {
            Debug.LogWarning("Knot name was empty when entering dialogue");
        }
        ContinueOrExitStory(); // begins the dialogue
    }
    private void EnterDialogue(string knotName) // begins or continues dialogue
    {
        if (_dialoguePlaying)
        {
            ContinueDialogue(); // continue dialogue if there is dialogue playing

        } else
        {
            BeginDialogue(knotName);
        }
        
        
    }

    private void ContinueOrExitStory() // updates the dialogue lines
    {
     

        if (_story.canContinue)
        {
            string dialogueLine = _story.Continue();

            _dialogueData.DialogueLine = dialogueLine; // update the scriptable object's line


            if (IsThereStoryChoices())
            {
                ShowChoices(); // debugg.
            //    _dialogue.Choiceslist = _story.currentChoices; 
            }
            
            
        } else // when there is no more dialogue lines in the story, exit
        {
            ExitDialogue();
        }

        
    }

    private bool IsThereStoryChoices()
    {
        return _story.currentChoices.Count > 0; // if there are choices in the story

    }

    private void ShowChoices()
    {
        foreach (var choice in _story.currentChoices)
        {
            Debug.Log(choice.text);
        }
    }

    private void ExitDialogue()
    {
        _dialoguePlaying = false;

        _userInterfaceData.ToggleUserInterface(UserInterfaces.Dialogue);

        _dialogueData.DialogueLine = "";

        _story.ResetState();

        _dialogueData.ExitDialogue();
    }



    private IEnumerator AutomaticallyContinueStory()
    {
        yield return new WaitForSeconds(_continueDialogueInSeconds);
        ContinueOrExitStory();
    }

}
