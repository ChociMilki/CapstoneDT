
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

/// <summary>
/// tt: stores and manages state of Ink global variables  using the observer pattern 
/// </summary>
public class DialogueVariables 
{
   public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    //for compilation - CAN BE OMITTED IF WORKING W/O  - refactored in next sequence 

    public DialogueVariables( TextAsset loadGlobalsJSON)
    {
        // create the dv story 
        Story globalVariablesStory = new Story(loadGlobalsJSON.text); 


        // dictionary initialization 
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
            {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name); 
            variables.Add(name, value);
            Debug.Log("Initalized global dialogue variable : " + name + " = " + value); 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="story"></param>
    public void StartListening(Story story)
    {
        // VS must be before assigning the listener! otherwise compilier error
        VariablesToStory(story); 
        story.variablesState.variableChangedEvent  += VariableChanged;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="story"></param>
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
   private void VariableChanged(string name, Ink.Runtime.Object value)
    {
           // only maintain variables that were initialized from the globals ink file , maintains current state of variables
           if(variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value); 
        }
 //       Debug.Log("Variable changed: " + name + " = " + value); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="story"></param>

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
       
        

}
