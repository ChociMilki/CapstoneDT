using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citiesnpc : MonoBehaviour
{
    [Header("City Color Configurations")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color berlinColor = Color.red;
    [SerializeField] private Color amsterdamColor = Color.green;
    [SerializeField] private Color tokyoColor = Color.blue;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        SwitchCity(); 
    }

    public void SwitchCity()
    {
        string cityName = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariableState("city_name")).value;

        // change of color of npc trigger cue based on which ink var is selected 

        switch (cityName)
        {
            case " ":
                spriteRenderer.color = defaultColor;
                break;
            case "Berlin":
                spriteRenderer.color = berlinColor;
                break;
            case "Amsterdam":
                spriteRenderer.color = amsterdamColor;
                break;
            case "Tokyo":
                spriteRenderer.color = tokyoColor;
                break;
            default:
                Debug.Log("City name not handled by switch statement: " + cityName);
                break;
        }
    }

}

