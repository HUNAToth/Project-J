using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private GameObject player;
    private int playerScore = 0;
    private int playerLevel = 1;
    private int playerExperience = 0;



    // - Initial - //
    /**********************************************************************************************/
    // Start is called before the first frame update
    // set the max health of the player and the current health of the player
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        maxHealth = healthLevel * 10;
        currentHealth = maxHealth;
        stamina = maxStamina;
    }



    // - Health - //
    /**********************************************************************************************/
    // Check the player is can pickup the health item
    public bool CanPickupHealthItem(int PointsRestored)
    {
        return currentHealth + PointsRestored <= 200;
    }





    // Disable the player scripts
    public void disablePlayerScript()
    {
        /* player.GetComponent<GunInventory>().enabled = false;
         player
             .GetComponent<GunInventory>()
             .currentGun
             .GetComponent<GunScript>()
             .enabled = false;
         player.GetComponent<PlayerMovementScript>().enabled = false;*/
    }

    //Enable the player scripts
    public void enablePlayerScript()
    {
        /*    player.GetComponent<GunInventory>().enabled = true;
            player
                .GetComponent<GunInventory>()
                .currentGun
                .GetComponent<GunScript>()
                .enabled = true;
            player.GetComponent<PlayerMovementScript>().enabled = true;*/

    }

    // - Armor - //
    /**********************************************************************************************/
    // Check the player is can pickup the armor item
    public bool CanPickupArmorItem(int PointsRestored, string ArmorType)
    {
        if (currentArmor < 100)
        {
            return true;
        }
        return false;
    }

    // Increase the armor of the player
    public void IncreaseArmor(int PointsRestored/*, string ArmorType*/)
    {
        currentArmor += PointsRestored;
    }

    // - Score - //
    /**********************************************************************************************/
    // Get the score of the player
    public int GetPlayerScore()
    {
        return playerScore;
    }
    // Set the score of the player
    public void SetPlayerScore(int _playerScore)
    {
        playerScore = _playerScore;
    }
    // Increase the level of the player
    public void IncreasePlayerLevel(int _playerLevel)
    {
        playerLevel += _playerLevel;
    }
    // Get the level of the player
    public int GetPlayerLevel()
    {
        return playerLevel;
    }
    // Increase the experience of the player
    public void IncreasePlayerExperience(int _playerExperience)
    {
        playerExperience += _playerExperience;
    }
    // Get the experience of the player
    public int GetPlayerExperience()
    {
        return playerExperience;
    }

    //Increase the Coins of the player
    public void IncreaseGold(int _playerCoins)
    {
        gold += _playerCoins;
    }

    //Get the Coins of the player
    public int GetPlayerGold()
    {
        return gold;
    }




}
