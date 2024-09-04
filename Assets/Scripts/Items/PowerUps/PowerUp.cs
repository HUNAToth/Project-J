using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    //this object is hovering up and then down
    public float hoverRange = 0.5f;
    public float hoverSpeed = 1.0f;
    public float pickUpRange = 0.1f;

    public bool isColliding = false;
    public string powerUpName = "DoubleJump";

    public int level = 10;
    public int amount = 10;

    public GameObject player;

    public EventLog eventLog;

    void Start()
    {
        //find the player by Tag
        player = GameObject.FindGameObjectWithTag("Player");
        //find the event log by name
        eventLog = GameObject.Find("EventLog").GetComponent<EventLog>();
    

        amount = Random.Range(level, level * level / 2);
    }

    void Update()
    {
        HandleHover();
        HandleCollision();
        if(isColliding){
            EnablePowerUp();
        }
    }

    void HandleHover(){
        //hover up and down
        transform.position = new Vector3(transform.position.x,
            transform.position.y + Mathf.Sin(Time.time * hoverSpeed) * hoverRange * Time.deltaTime,
            transform.position.z);
    }
    void HandleCollision(){
       if (Vector3.Distance(transform.position, player.transform.position) < pickUpRange)
        {
            isColliding = true;
            Debug.Log("PowerUp Colliding with player");
        }
    }

    void EnablePowerUp(){
        //enable the power up
        switch (powerUpName)
        {
            case "DoubleJump":
                player.GetComponentInChildren<HeroKnight>().double_jump_enabled = true;
                player.GetComponentInChildren<HeroKnight>().can_double_jump = true;
                break;
            case "WallGrab":
                player.GetComponentInChildren<HeroKnight>().wall_jump_enabled = true;
                player.GetComponentInChildren<HeroKnight>().can_wall_jump = true;
                break;
            case "HealthBonus":
                player.GetComponentInChildren<CharacterStats>().IncreaseHealth(amount);
                break;
            case "GoldBonus":
                player.GetComponentInChildren<PlayerStats>().IncreaseGold(amount);
                break;
            case "PowerBonus":
                player.GetComponentInChildren<PlayerStats>().IncreasePower(amount);
                break;
            case "XPBonus":
                player.GetComponentInChildren<PlayerStats>().IncreaseExperience(amount);
                break;
            case "ArmorBonus":
                player.GetComponentInChildren<PlayerStats>().IncreaseArmor(amount);
                break;
            default:
                break;
        }
        string eventText = "Picked up " + powerUpName;
        //if Bonus in powerUpName, add amount to eventText
        if (powerUpName.Contains("Bonus"))
        {
            eventText += " +" + amount;
        }
        EventLogItem newItem = new EventLogItem(eventText, Time.time);
        eventLog.AddEvent(newItem);

        //destroy self
        Destroy(gameObject);
    }
}
