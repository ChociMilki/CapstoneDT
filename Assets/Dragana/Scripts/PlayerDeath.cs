using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Call this method to trigger the death animation
    public void Die()
    {
        // Set the "IsDead" parameter to trigger the death animation
        animator.SetTrigger("IsDead");
    }
}
