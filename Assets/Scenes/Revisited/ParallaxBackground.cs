using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
/// <summary>
/// tt: handles background scroll effect within the "EnvironmentCanvas" 
///Using this multi canvas 2D UI approach, within Inspector,  I make sure to deactivate box collider (keep active component) so that the player does not run into the collider. 
/// </summary>
public class ParallaxBackground : MonoBehaviour
{
    [Header("Parallax Background Settings")]
    [SerializeField] private float backgroundMovementSpeed;
     private Button startStory; 
    private float colliderRepeatWidth;
    private Vector3 backgroundOriginPos;
    private int half = 2; 

   

    private void Start()
    {
  
        //set origin position to the transform 
       backgroundOriginPos = transform.position;

        // grab the box collider and split in half along x for parallax calculation
       colliderRepeatWidth = GetComponent<BoxCollider2D>().size.x / half;
     
    }
    void Update()
    {
        CalculateParallax(); 
    }

    /// <summary>
    ///  tt : move the game object at a certain speed in the -x direction 
    ///  if  half of the collider has passed then reset to origin position. 
    /// </summary>
    private void CalculateParallax()
    {
            transform.Translate(backgroundMovementSpeed * Time.deltaTime * Vector3.left);
            if (transform.position.x < backgroundOriginPos.x - colliderRepeatWidth)
            {
                transform.position = backgroundOriginPos;
            }
        }



    }

