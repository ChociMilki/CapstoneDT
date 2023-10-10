using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Movement Configurations")]
    [SerializeField] private float horizontalInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private float jumpPower = 4f;
    [SerializeField] private bool isJumping = false;

 


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();
        Jump();

    }

    private void FixedUpdate()
    {


        playerRB.velocity = new Vector2(horizontalInput * moveSpeed, playerRB.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
    /// <summary>
    ///  checks our bool to our horizontal input to flip based on whichever direction 
    /// </summary>
    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpPower);
            isJumping = true;
        }
    }
    /// <summary>
    ///  not working atm, called in U || FU
    /// </summary>
    void FreezePlayerMovementDuringDialogue()
    {
    //    if (DialogueManager.GetInstance().dialogueIsPlaying)
    //    {
    //        return;
    //    }
    }

}
