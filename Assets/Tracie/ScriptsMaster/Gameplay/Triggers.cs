using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// tt : handles trigger related game events , observer pattern
/// </summary>
public class Triggers : MonoBehaviour
{
    /// <summary>
    ///  when trigger destroyed tell game manager
    /// </summary>
    /// <param name="collision"></param>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.NotifyCollision(collision); 
        Destroy(this.gameObject);
        Debug.Log("Trigger destroyed"); 
    }
}
