using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private float sjfBackgroundMovementSpeed;
    private float sjfBackgroundRepeatWidth;
    private Vector3 sjfBackgroundStartPos;
    private int half = 2;

    private void Start()
    {
        // sets sjfbackground endless scroll 
        sjfBackgroundStartPos = transform.position;
        sjfBackgroundRepeatWidth = GetComponent<BoxCollider2D>().size.x / half;
        Debug.Log("background endless scroll");
    }
    void Update()
    {
        CalculateParallax(); 
    }
    private void CalculateParallax()
    {
            transform.Translate(sjfBackgroundMovementSpeed * Time.deltaTime * Vector3.left);
            if (transform.position.x < sjfBackgroundStartPos.x - sjfBackgroundRepeatWidth)
            {
                transform.position = sjfBackgroundStartPos;
            }
        }



    }

