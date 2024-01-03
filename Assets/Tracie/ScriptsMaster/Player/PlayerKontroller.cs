using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKontroller : MonoBehaviour
{

    [Header("Player Movement Configurations")]
    [SerializeField] private float horizontalInput; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] SpriteRenderer playerSprite; 

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();  
    }
    void Update()
    {
        
        LetsGetGoing();
        FlipSprite();
    }

    private void LetsGetGoing()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = transform.right * horizontalInput;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);
        Debug.Log("moving "); 
    }
    private void FlipSprite()
    {
        // Flip the sprite based on the direction
        if (horizontalInput < 0)
        {
            playerSprite.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            playerSprite.flipX = false;
        }
    }


    //void FreezePlayerMovementDuringDialogue()
    //{
    //    //    if (DialogueManager.GetInstance().dialogueIsPlaying)
    //    //    {
    //    //        return;
    //    //    }
    //}


}







