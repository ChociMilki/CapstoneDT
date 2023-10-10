using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false; 

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // NEXT STEPS - ADVANCE QUEST FORWARD NOW THAT STEP IS FINISHED 

            Destroy(this.gameObject); 
        }
    }
}
