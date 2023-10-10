using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [Header("Gem Configurations")]
    [SerializeField] private float respawnTimeSeconds = 8;
    [SerializeField] private int experienceGained = 25;
    private CircleCollider2D gemCircleCollider;
    private SpriteRenderer gemVisual;

    private void Awake()
    {
        gemCircleCollider = GetComponent<CircleCollider2D>();
        gemVisual = GetComponentInChildren<SpriteRenderer>();
    }

    private void CollectGem()
    {
        gemCircleCollider.enabled = false; 
        gemVisual.gameObject.SetActive(false);
        GameEventsMaster.instance.playerEvents.ExperienceGained(experienceGained);
        GameEventsMaster.instance.miscEvents.GemCollected();
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTimeGem()); 
    }

    private IEnumerator RespawnAfterTimeGem()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        gemCircleCollider.enabled = true;
        gemVisual.gameObject.SetActive(true); 
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.CompareTag("Player"))
        {
            CollectGem(); 
        }
    }


}
