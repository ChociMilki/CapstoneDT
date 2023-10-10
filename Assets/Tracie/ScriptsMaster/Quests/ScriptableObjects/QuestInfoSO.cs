using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestInfoSO" , menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    // control serialization of a private field indirectly through a property
    [field: SerializeField] public string id { get; private set; }
    [Header("General")]
    public string displayName; // how we want the quest name to appear in game

    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int experienceReward; 

    //in order to be unique to each quest , using the name of the scriptable object for the ID 
    // again, ensure the id is always the name of the Scriptable Object asset 

    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name; 
        UnityEditor.EditorUtility.SetDirty(this);
#endif


    }


}
