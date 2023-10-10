using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsMaster : MonoBehaviour
{
    public static GameEventsMaster instance { get; private set; }

    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public GoldEvents goldEvents;
    public MiscEvents miscEvents;
    public QuestEvents questEvents; 
    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); return; }
        instance = this;

        // initialize all events 
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        goldEvents = new GoldEvents();
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();


        
    }
    public static GameEventsMaster GetInstance()
    {
        return instance;
    }
}
