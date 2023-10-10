using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// tt: enum to hold battle state 
/// </summary>
public enum BattleState
{
    Start, 
    Waiting, 
    PlayerTurn, 
    EnemyTurn, 
    Won, 
    Lost
}

/// <summary>
/// tt: battle system
/// </summary>
public class BattleSystem : MonoBehaviour
{
    [Header("Battle System Configurations")]
   [SerializeField] private BattleState state;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform playerStation;
    [SerializeField] private Transform bossStation;
    [SerializeField] private TextMeshProUGUI shitTalkinText;
   [SerializeField] private  TextMeshProUGUI playerintText;
    [SerializeField] private TextMeshProUGUI bossintText;

    private float coroutineWaitTime = 6f;
    private float preventPlayerAction = 6f;
    private float inbetweenWaiting = 10f; 
    private int recoverAmount = 8;

    Combatant playerC; 
    Combatant bossC;

    public BattlePanel playerPanel; 
    public BattlePanel bossPanel;
    

    /// <summary>
    /// tt: set the state and initalize 
    /// </summary>
    private void Start()
    {
        state = BattleState.Start;
        StartCoroutine(InitializeBattle()); 
    }



    /// <summary>
    ///tt:  initializes battle and gets info on combatatants
    /// </summary>
    private IEnumerator InitializeBattle()
    {

        //player
       GameObject player =  Instantiate(playerPrefab, playerStation);
         playerC =     player.GetComponent<Combatant>(); 

        // boss
        GameObject boss = Instantiate(bossPrefab, bossStation);
        bossC = boss.GetComponent<Combatant>();
        Debug.Log("Battle initialized");

        //initializes dialogue during battle - boss first 
        shitTalkinText.text = "BEHOLD " +  bossC.combatantName  + " IS UPON YOU";

        // NEED: One shot audio for boss arrival // battle initalization 

        // DRAGANA 
        Debug.Log(" Dragana opening animationsss");
       


        // setup panels 
        playerPanel.SetBattleUI(playerC);
        bossPanel.SetBattleUI(bossC);

        // coroutine 
        yield return new WaitForSeconds(coroutineWaitTime); 

        // change state to player
        state = BattleState.PlayerTurn;
        PlayerTurn(); 
    }


    /// <summary>
    /// tt: handles player turn ui 
    /// </summary>
    private void PlayerTurn()
    {
        shitTalkinText.text = " The decision is yours! Choose your next tactic."; 
    }


    /// <summary>
    ///  tt: handles simple attack selection
    /// </summary>
    public  void OnFightButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerAttackBasic()); 
    }

    /// <summary>
    /// tt: handles recover hp 
    /// </summary>
    public void OnRecoverButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerRecover()); 
    }

    /// <summary>
    ///  tt: handles strong attack 
    /// </summary>
    public void OnStrongAttackButton()
    {

        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerStrongAttack()); 
        // NEED : One shot audio for strong attack 


        // DRAGANA 
        Debug.Log(" Dragana  strong attack animationsss");
    }


    /// <summary>
    ///  tt : handles hp and hp 2 text 
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerRecover()
    {
        playerC.Recover(recoverAmount);
        playerPanel.TrackHP(playerC.currentHP);

        //update ui 
        playerintText.text = playerC.currentHP.ToString();
        shitTalkinText.text = " The heavens have blessed you. Go forth and conquer! ";

        //  NEED : Stylistic event 

        yield return new WaitForSeconds(preventPlayerAction);
       

        // next state 

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn()); 
    }
    






    /// <summary>
    /// tt : handles attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerAttackBasic()
    {
        // do basic damage 

      bool isDead =   bossC.TakeDamage(playerC.damage);
        bossPanel.TrackHP(bossC.currentHP);
       
        //update ui 
        bossintText.text = bossC.currentHP.ToString(); 
        shitTalkinText.text = bossC.combatantName + " has been Felled! "  ;



        // DRAGANA 
        Debug.Log(" Dragana player deals damage animationsss");

        yield return new WaitForSeconds(preventPlayerAction);


        // has died? 
        if (isDead)
        {
            // end battle through slaying boss 
            state = BattleState.Won;
            EndBattle(); 
        }
        else
        {
            //  if !isDead then  enemy turn 
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn()); 
        }
    }


    /// <summary>
    /// tt: strong attack handle 
    /// </summary>
    /// <returns></returns>
    private IEnumerator  PlayerStrongAttack()
    {
        // do strong attack damage 
        bool isDead = bossC.TakeStrongAttackDamage(playerC.strongAttackDamage); 
         bossPanel.TrackHP(bossC.currentHP);
        // update ui 
        bossintText.text = bossC.currentHP.ToString(); 
        shitTalkinText.text = bossC.combatantName + " has been gravely wounded!";

        // DRAGANA 
        Debug.Log(" Dragana player deals STRONG ATTACK  damage animationsss");

        yield return new WaitForSeconds(preventPlayerAction);

        // has died? 
        if (isDead)
        {
            // end battle through slaying boss 
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            //  if !isDead then  enemy turn 
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }



    /// <summary>
    ///  GET HELP - BOSS 
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemyTurn()
    {
        // INCREASE  BUFF BASED ON BOSS IN PREFABS 

        shitTalkinText.text = bossC.combatantName + " strikes with a rageful glory! ";

        // DRAGANA 

        Debug.Log(" Dragana enemy strikes  animationsss");


        // deal damage 
        bool isDead =    playerC.TakeDamage(bossC.damage); 
       

        // update ui
        playerPanel.TrackHP(playerC.currentHP);
        playerintText.text = playerC.currentHP.ToString(); 

        yield return new WaitForSeconds(coroutineWaitTime);

        if(isDead)
        {
            state = BattleState.Lost;
            EndBattle();
            
        }
        else
        {
            state = BattleState.PlayerTurn;
          
            PlayerTurn();
     
        }

    }



    /// <summary>
    /// tt: handles scene load dependent on outcome condition
    /// </summary>
    private void EndBattle()
    {
       
        if (state == BattleState.Won)
        {
            shitTalkinText.text = " You have excorsized this terrible scourge... ";
            Debug.Log(" win ");
      //      StartCoroutine(Wait());
            GameManager.GetInstance().EnterWinningMoment();
       //     StopCoroutine(Wait()); 

        }
        else if( state == BattleState.Lost)
            {
            shitTalkinText.text = " You have proven NO match for " + bossC.combatantName;
            Debug.Log(" lose");
            GameManager.GetInstance().LosingLost();

            // DRAGANA 
            Debug.Log(" Dragana end battle animations"); 
        }
    }




    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(inbetweenWaiting);
    }


}
