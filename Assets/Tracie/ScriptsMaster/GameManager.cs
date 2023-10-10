using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// tt : 
/// </summary>
public class GameManager : MonoBehaviour
{ private static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); return; }
        instance = this;
    } 

    public static GameManager GetInstance()
    {
        return instance; 
    }

     [Header(" GameManager Configurations")]
    [SerializeField] private GameObject npcSelected;


    // events 
    // event raised when collision occurs 
    public static event System.Action<Collision2D> OCEEvent; 
    // notify listeners about collision with this method , not accessed through singleton
    public static void NotifyCollision(Collision2D notifyCollision)
    {
        OCEEvent?.Invoke(notifyCollision);
    }
    private void OnEnable()
    {
        // subscribe to the collison event when GM enabled on awake 
        OCEEvent += CallNPC;      
    }

    ///// <summary>
    /////  need to check when to unsubscribe
    ///// </summary>
    //private void OnDisbale()
    //{
    //    OCEEvent -= CallNPC;
    //}


    /// <summary>
    ///  call npc to player 
    /// </summary>
    public void  CallNPC(Collision2D collison)
    {
        // calls npcs movement script 
        npcSelected.GetComponent<NPC>().NPCFollowPlayer(); 
    }
}
