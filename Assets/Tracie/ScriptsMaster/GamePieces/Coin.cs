using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    [Header("Coin Configurations")]
    [SerializeField] private float respawnTimeSeconds = 8;
    [SerializeField] private int goldGained = 1;

    private CircleCollider2D circleCollider;
    private SpriteRenderer visual;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        visual = GetComponent<SpriteRenderer>();
    }

    private void CollectCoin()
    {
        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsMaster.instance.goldEvents.GoldGained(goldGained);
        GameEventsMaster.instance.miscEvents.CoinCollected();
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTimeCoin());
    }

    private IEnumerator RespawnAfterTimeCoin()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        circleCollider.enabled = true; 
        visual.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            CollectCoin(); 
        }
    }

}
