using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// tt : handles npc functionalities 
/// </summary>
public class NPC : MonoBehaviour
{
    [Header(" NPC Configurations")]
    [SerializeField] private Transform player;
    [SerializeField] private float npcfollowSpeed;
    [SerializeField] private float npcstoppingDistance;


    /// <summary>
    ///  npc goes from static to players location and stops when they are x proximity from the player 
    /// </summary>
    public void NPCFollowPlayer()
    {
        if (player == null) { return; }
        // calc distance between npc and player 
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // check if npc is outside of stopping distance 
        if (distanceToPlayer >= npcstoppingDistance)
        {
            //calc direction from npc to player 
            Vector2 directionToPlayer = player.position - transform.position;
            // normalize 
            directionToPlayer.Normalize();
            // move npc towards player at x speed 
            transform.Translate(Vector2.left * npcfollowSpeed * Time.deltaTime * directionToPlayer); 
        }
        Debug.Log("Npc on the move"); 
    }
}
