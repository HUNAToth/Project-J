using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ActiveScreen;
    public GameObject GamePlayScreen;
    public GameObject StatScreen;
    //TODO MORE SCREENS
    public bool screenOpen = false;
    public GameObject player;
    public GameObject UI_HealthDisplay;
    public GameObject UI_StaminaDisplay;
    public GameObject UI_PowerDisplay;
    public GameObject UI_GoldDisplay;
    public GameObject UI_XPDisplay;
    public GameObject UI_LevelDisplay;
    public GameObject UI_FreeStatDisplay;

    [Header("Stat Screen")]
    public GameObject UI_FreeSkillPointDisplay;
    public GameObject UI_HealthLevelDisplay;
    public GameObject UI_DamageLevelDisplay;
    public GameObject UI_SpeedLevelDisplay;
    public GameObject UI_ArmorLevelDisplay;
    public GameObject UI_PowerLevelDisplay;
    public GameObject UI_StaminaLevelDisplay;

    [Header("Stat Screen Buttons")]
    public GameObject UI_HealthLevelButton;
    public GameObject UI_DamageLevelButton;
    public GameObject UI_SpeedLevelButton;
    public GameObject UI_ArmorLevelButton;
    public GameObject UI_PowerLevelButton;
    public GameObject UI_StaminaLevelButton;

    [Header("Calculated Stat texts")]
    public GameObject UI_HP_Calc_Text;
    public GameObject UI_Damage_Calc_Text;
    public GameObject UI_Speed_Calc_Text;
    public GameObject UI_Armor_Calc_Text;
    public GameObject UI_Power_Calc_Text;
    public GameObject UI_Stamina_Calc_Text;

    private CharacterStats playerStats;





    void Start()
    {
        ActiveScreen = GamePlayScreen;
        GamePlayScreen.SetActive(true);
        StatScreen.SetActive(false);
        playerStats = player.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.Tab)){
            screenOpen = !screenOpen;
            if(screenOpen)
            {
                GamePlayScreen.SetActive(false);
                StatScreen.SetActive(true);
                ActiveScreen = StatScreen;
            }
            else
            {
                StatScreen.SetActive(false);
                GamePlayScreen.SetActive(true);
                ActiveScreen = GamePlayScreen;
            }
           
        }
        if(ActiveScreen == GamePlayScreen){
            UpdateLevelDisplay(playerStats.GetLevel());
            UpdateFreeStatDisplay(playerStats.GetFreeStatPoints());

            UpdateHealthDisplay(playerStats.GetCurrentHealth(), 
                                playerStats.GetMaxHealth()
                                );
            UpdateStaminaDisplay(playerStats.GetCurrentStamina(),
                                playerStats.GetMaxStamina()
                                );
                                
            UpdatePowerDisplay(playerStats.GetCurrentPower(),
                                playerStats.GetMaxPower()
                                );

            UpdateGoldDisplay(playerStats.GetCurrentGold());
            UpdateXPDisplay(playerStats.GetCurrentXP(),
                            playerStats.GetXPToNextLevel());
      
        
        }
        else if(ActiveScreen == StatScreen){
            UI_FreeSkillPointDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = playerStats.GetFreeStatPoints().ToString();
           if (playerStats.GetFreeStatPoints() > 0)
            {
                UI_HealthLevelButton.SetActive(true);
                UI_DamageLevelButton.SetActive(true);
               /* UI_SpeedLevelButton.SetActive(true);
                UI_ArmorLevelButton.SetActive(true);
                UI_PowerLevelButton.SetActive(true);
                UI_StaminaLevelButton.SetActive(true);*/
            }
            else
            {
                UI_HealthLevelButton.SetActive(false);
                UI_DamageLevelButton.SetActive(false);
                UI_SpeedLevelButton.SetActive(false);
                UI_ArmorLevelButton.SetActive(false);
                UI_PowerLevelButton.SetActive(false);
                UI_StaminaLevelButton.SetActive(false);
            }


            UpdateStatDisplay(
                //"Health_LVL",
                playerStats.GetHealthLevel(),
                //"Damage_LVL",
                playerStats.GetDamageLevel()
            );
            UpdateCalculatedHPTextDisplay(playerStats.GetHealthLevel());
            UpdateCalculatedDMGTextDisplay(playerStats.GetDamageLevel());

        }

    }

    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        string healthText = currentHealth.ToString();
        string maxHealthText = maxHealth.ToString();
        //UI_HealthDisplay has a TextMeshProUGUI component, update the text
        UI_HealthDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = healthText + "/" + maxHealthText;
    }
    private void UpdateStaminaDisplay(int currentStamina, int maxStamina)
    {
        string staminaText = currentStamina.ToString();
        string maxStaminaText = maxStamina.ToString();
        //UI_StaminaDisplay has a TextMeshProUGUI component, update the text
        UI_StaminaDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = staminaText + "/" + maxStaminaText;
    }
    private void UpdatePowerDisplay(int currentPower, int maxPower)
    {
        string powerText = currentPower.ToString();
        string maxPowerText = maxPower.ToString();
        //UI_PowerDisplay has a TextMeshProUGUI component, update the text
        UI_PowerDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = powerText + "/" + maxPowerText;
    }

    private void UpdateGoldDisplay(int currentGold)
    {
        string goldText = currentGold.ToString();
        //UI_GoldDisplay has a TextMeshProUGUI component, update the text
        UI_GoldDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = goldText;
    }

    private void UpdateXPDisplay(int currentXP, int XPToNextLevel)
    {
        string XPText = currentXP.ToString();
        string XPToNextLevelText = XPToNextLevel.ToString();
        UI_XPDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = XPText + "/" + XPToNextLevelText;
    }

    private void UpdateLevelDisplay(int level)
    {
        string levelText = level.ToString();
        UI_LevelDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = levelText;
    }

    private void UpdateFreeStatDisplay(int freeStatPoints)
    {
        string freeStatPointsText = freeStatPoints.ToString();
        UI_FreeStatDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = freeStatPointsText;
    }

    private void UpdateStatDisplay(int healthLevel, int damageLevel)
    {
        string healthLevelText = healthLevel.ToString();
        string damageLevelText = damageLevel.ToString();
        UI_HealthLevelDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = healthLevelText;
        UI_DamageLevelDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = damageLevelText;
    }

    private void UpdateCalculatedHPTextDisplay(int healthLevel)
    {   
        //writes the calculated maxHP to the UI
        //in the format: "Max HP: 100 + the amount of HP from the health level"
        string calculatedHPText = playerStats.GetMaxHealth().ToString();
        calculatedHPText += " -> ";
        calculatedHPText += playerStats.CalculateMaxHealthFromHealthLevel(healthLevel+1);
        UI_HP_Calc_Text.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedHPText;

    }
    private void UpdateCalculatedDMGTextDisplay(int damageLevel)
    {
      //writes the calculated damage to the UI
      //format: "Damage: "+weapon_damage+" + "+damageLevel*damage_per_level
        string calculatedDMGText = ""+(playerStats.GetAttackValue()+playerStats.GetWeaponDamage());
        calculatedDMGText += " -> ";
        calculatedDMGText += playerStats.GetAttackValueFromDamageLevel(damageLevel+1);
        UI_Damage_Calc_Text.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedDMGText;

    }

    public void IncreaseHealthLevel()
    {
        playerStats.IncreaseHealthLevel();
    }
    public void IncreaseDamageLevel()
    {
        playerStats.IncreaseDamageLevel();
    }
    public void IncreaseSpeedLevel()
    {
        playerStats.IncreaseDexLevel();
    }

    public void IncreaseArmorLevel()
    {
        playerStats.IncreaseArmorLevel();
    }
    public void IncreasePowerLevel(){
        playerStats.IncreasePowerLevel();
    }
    public void IncreaseStaminaLevel(){
        playerStats.IncreaseStaminaLevel();
    }    

}
