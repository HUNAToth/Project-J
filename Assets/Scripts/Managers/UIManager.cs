using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ActiveScreen;
    public GameObject GamePlayScreen;
    public GameObject StatScreen;
    //TODO MORE SCREENS
    public bool isStatScreenOpen = false;
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
        HandleStatScreenStatus();
        HandleGamePlayScreenStats();
        HandleStatusScreenStats();
    }

    private void HandleStatScreenStatus()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            isStatScreenOpen = !isStatScreenOpen;
            HandleStatScreenOpening();
        }
    }

    public void HandleTabPress()
    {
        isStatScreenOpen = !isStatScreenOpen;
        HandleStatScreenOpening();
    }

    private void HandleGamePlayScreenStats()
    {
        if (ActiveScreen == GamePlayScreen)
        {
            UpdateLevelDisplay();
            UpdateFreeStatDisplay();
            UpdateHealthDisplay();
            UpdateStaminaDisplay();
            UpdatePowerDisplay();
            UpdateGoldDisplay();
            UpdateXPDisplay();
        }
    }

    private void HandleStatusScreenStats()
    {
        if (ActiveScreen == StatScreen)
        {
            UpdateFreeSkillPointDisplay();

            SetStatButtonIntractability();

            UpdateHealthLevelDisplay();
            UpdateDamageLevelDisplay();

            UpdateCalculatedHPTextDisplay();
            UpdateCalculatedDMGTextDisplay();
        }
    }

    public void HandleStatScreenOpening()
    {
        if (isStatScreenOpen)
        {
            GamePlayScreen.SetActive(false);
            StatScreen.SetActive(true);
            ActiveScreen = StatScreen;
            UI_HealthLevelButton = GameObject.Find("IncreaseHealthLevelBTN");
            UI_DamageLevelButton = GameObject.Find("IncreaseDMGLevelBTN");
        }
        else
        {
            StatScreen.SetActive(false);
            GamePlayScreen.SetActive(true);
            ActiveScreen = GamePlayScreen;
        }
    }

    private void SetStatButtonIntractability()
    {
        bool value = playerStats.GetFreeStatPoints() > 0;
        UI_HealthLevelButton.GetComponent<Button>().interactable = value;
        UI_DamageLevelButton.GetComponent<Button>().interactable = value;
    }

    public void QuitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    private void SetUILabelText(GameObject label, string text)
    {
        label.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    private void UpdateFreeSkillPointDisplay()
    {
        SetUILabelText(UI_FreeSkillPointDisplay, playerStats.GetFreeStatPoints().ToString());
    }

    private void UpdateHealthDisplay()
    {
        SetUILabelText(UI_HealthDisplay,
                        playerStats.GetCurrentHealth().ToString() + "/" + playerStats.GetMaxHealth().ToString()
        );
    }

    private void UpdateStaminaDisplay()
    {
        SetUILabelText(UI_StaminaDisplay,
                        playerStats.GetCurrentStamina().ToString() + "/" + playerStats.GetMaxStamina().ToString()
        );

    }
    private void UpdatePowerDisplay()
    {
        SetUILabelText(UI_PowerDisplay,
                        playerStats.GetCurrentPower().ToString() + "/" + playerStats.GetMaxPower().ToString()
        );
    }

    private void UpdateGoldDisplay()
    {
        SetUILabelText(UI_GoldDisplay, playerStats.GetCurrentGold().ToString());
    }

    private void UpdateXPDisplay()
    {
        SetUILabelText(UI_XPDisplay,
            playerStats.GetCurrentXP().ToString() + "/" + playerStats.GetXPToNextLevel().ToString()
        );
    }

    private void UpdateLevelDisplay()
    {
        SetUILabelText(UI_LevelDisplay, playerStats.GetLevel().ToString());
    }

    private void UpdateFreeStatDisplay()
    {
        SetUILabelText(UI_FreeStatDisplay, playerStats.GetFreeStatPoints().ToString());
    }

    private void UpdateHealthLevelDisplay()
    {
        SetUILabelText(UI_HealthLevelDisplay, playerStats.GetHealthLevel().ToString());
    }

    private void UpdateDamageLevelDisplay()
    {
        SetUILabelText(UI_DamageLevelDisplay, playerStats.GetDamageLevel().ToString());
    }
    private void UpdateCalculatedHPTextDisplay()
    {
        //writes the calculated maxHP to the UI
        //in the format: "Max HP: 100 + the amount of HP from the health level"
        string calculatedHPText = playerStats.GetMaxHealth().ToString();
        calculatedHPText += " -> ";
        calculatedHPText += playerStats.CalculateMaxHealthFromHealthLevel(playerStats.GetHealthLevel() + 1);
        SetUILabelText(UI_HP_Calc_Text, calculatedHPText);
    }
    private void UpdateCalculatedDMGTextDisplay()
    {
        //writes the calculated damage to the UI
        //format: "Damage: "+weapon_damage+" + "+damageLevel*damage_per_level
        string calculatedDMGText = "" + (playerStats.GetAttackValue() + playerStats.GetWeaponDamage());
        calculatedDMGText += " -> ";
        calculatedDMGText += playerStats.GetAttackValueFromDamageLevel(playerStats.GetDamageLevel() + 1);
        SetUILabelText(UI_Damage_Calc_Text, calculatedDMGText);
    }



    //used in editor ui buttons
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
    public void IncreasePowerLevel()
    {
        playerStats.IncreasePowerLevel();
    }
    public void IncreaseStaminaLevel()
    {
        playerStats.IncreaseStaminaLevel();
    }

}
