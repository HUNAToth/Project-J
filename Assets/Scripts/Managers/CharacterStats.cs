using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Skills")]
    // - General Stats - 
    [SerializeField]
    public int level = 1;
    

    //stat to calculate max health
    public int healthLevel = 1;

    //stat to calculate max damage
    public int damageLevel = 1;

    //stat to calculate max dex: speed, attack speed, attack range(?)
    public int dexLevel = 1;

    //stat to calculate max armor
    public int armorLevel = 1;

    //stat to calculate max power (the mana of the player)
    public int powerLevel = 1;
    //stat to calculate max stamina (the stamina of the player)
    public int staminaLevel = 1;

    public int freeStatPoints = 0;



    [Header("Health")]
    // - Health -
    [SerializeField]
    public int maxHealth;
    public int currentHealth;

    public int healthIncreasePerLevel = 10;
    public int damageIncreasePerLevel = 1;

    protected bool isDead = false;


    [Header("Magic")]
    public int currentPower = 0;
    public int maxPower = 100;

    [Header("Stamina")]
    public int stamina = 0;
    public int staminaRegen = 0;
    public int maxStamina = 100;


    [Header("Inventory")]
    public int gold = 0;

    protected float TickTime = 0.01f;
    protected float TickTimeDelta = 0.0f;

    public GameObject LastSeenEnemy;

    // - Armor -
    [SerializeField]
    protected int maxArmor;
    public int currentArmor;
    [SerializeField]
    public float attackRange = 2.5f;
    [SerializeField]
    public float rangedAttackRange = 0.0f;



    //TODO create CharacterTeam enum file, now its in NPCBehaviour.cs too
    public enum CharacterTeam
    {
        Player,
        Enemy,
        Enemy2,
        Neutral,
    }

    public CharacterTeam selfTeam;

    public GameObject LevelUpParticleEmitter;


    [Header("Combat")]
    [SerializeField]
    public int attackDamage = 10;

    [SerializeField]
    public float attackCooldown = 3f;

    public float defaultCritChancePct = 5.0f;


    [Header("Movement")]


    [SerializeField]
    public float movementSpeed = 1f;
    [SerializeField]
    public float chaseSpeed = 5f;

    [SerializeField]
    public float sightRange = 10f;

    [SerializeField]
    public float patrolRange = 1.5f;


    [SerializeField]
    public string EnemyType = "Melee";
    public float fireballSpeed = 10f;
    public Animator m_animator;

    [SerializeField]
    public HealthBar healthBar;
    public float blinkDeltaTime = 0.2f;
    public float blinkDelta = 0.0f;

    public bool isVisible = true;

    public float destroySelfTime = 2.5f;


    [Header("XP")]

    public int XP = 0;

    public int XPToNextLevel = 0;

    
    public int lootSize = 1;
    public int lootLevel = 1;

    public GameObject[] loot;


    public EventLog eventLog;


    void Awake()
    {
        eventLog = GameObject.Find("EventLog").GetComponent<EventLog>();
    }
    void Start()
    {
        m_animator = GetComponent<Animator>();
        maxHealth = SetMaxHealthFromHealthLevel(healthLevel);
        currentHealth = maxHealth;
        maxArmor = SetMaxArmorFromArmorLevel();
        currentArmor = maxArmor;
        staminaRegen = 15;
        XPToNextLevel = CalculateXpToNextLevel(level);
        currentHealth = maxHealth;
        currentArmor = maxArmor;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetHealthBar(maxHealth, currentHealth);
        healthBar.SetArmorBar(maxArmor, currentArmor);

        movementSpeed += Random.Range(0.0f, 0.25f);
        chaseSpeed += Random.Range(0.0f, 0.25f);

        loot = new GameObject[lootSize];
        //TODO create a random loot BEFORE the enemy dies
        for (int i = 0; i < lootSize; i++)
        {
            //create a random power up with PowerUpFactory 
            //and add it to the loot 
           // loot[i] = PowerUpFactory.CreateRandomPowerUp(lootLevel);
        }

    }


    private int SetMaxHealthFromHealthLevel(int _healthLevel)
    {
        maxHealth = CalculateMaxHealthFromHealthLevel(_healthLevel);
        return maxHealth;
    }
    public  int CalculateMaxHealthFromHealthLevel(int _healthLevel)
    {
        int TmpMaxHealth = _healthLevel * healthIncreasePerLevel + level;
        return TmpMaxHealth;
    }
    private int SetMaxArmorFromArmorLevel()
    {
        maxArmor = level + armorLevel;
        return maxArmor;
    }


    private void Update()
    {
        if (isDead)
        {
            Blink();
        }else
        {
            XPToNextLevel = CalculateXpToNextLevel(level);
            TickTimeDelta += Time.deltaTime;
            RegenStamina();
        }
   
    }

    public void RegenStamina()
    {
        if (TickTimeDelta >= TickTime)
        {
            TickTimeDelta = 0f;
            if (stamina + staminaRegen <= maxStamina)
            {
                stamina += staminaRegen;
            }
            else
            {
                stamina = maxStamina;
            }
        }
    }


        // Set the last seen enemy
        public void SetLastSeenEnemy(GameObject enemy)
    {
        LastSeenEnemy = enemy;
    }

    // Get the last seen enemy
    public GameObject GetLastSeenEnemy()
    {
        return LastSeenEnemy;
    }

    /**********************************************************************************************/
    // - Health -
    
    // Get the max health of the character
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    // Set current health of the character
    public void SetCurrentHealth(int newCurrent)
    {
        currentHealth = newCurrent;
    }

    public void IncreaseHealth(int amount){
        currentHealth += amount;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }
    // Get current health of the character
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetHealthLevel()
    {
        return healthLevel;
    }
    // Get the isDead the character
      public bool GetIsDead()
    {
        return isDead;
    }
    // Set the isDead the character
      public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
    }

    /**********************************************************************************************/
    // - Armor -

    // Get the max armor of the character
    public int GetMaxArmor()
    {
        return maxArmor;
    }

    // Set the current armor of the character
    public void SetCurrentArmor(int newCurrent)
    {
        currentArmor = newCurrent;
    }

    // Get the current armor of the character
    public int GetCurrentArmor()
    {
        return currentArmor;
    }

    /**********************************************************************************************/
    // - Gold -
    public int GetCurrentGold()
    {
        return gold;
    }
    public void SetCurrentGold(int newCurrent)
    {
        gold = newCurrent;
    }
    /**********************************************************************************************/
    // - Power -
    public int GetCurrentPower()
    {
        return currentPower;
    }
    public void SetCurrentPower(int newCurrent)
    {
        currentPower = newCurrent;
    }
    public void IncreasePower(int _power)
    {
        currentPower += _power;
    }
    public int GetMaxPower(){
        return maxPower;
    }
    /**********************************************************************************************/
    // - Stamina -
    public int GetCurrentStamina()
    {
        return stamina;
    }
    public int GetMaxStamina()
    {
        return maxStamina;
    }
    public void SetCurrentStamina(int newCurrent)
    {
        stamina = newCurrent;
    }

    public int GetCurrentXP()
    {
        return  XP;
    }
    public int GetXPToNextLevel()
    {
        return this.XPToNextLevel;
    }

    public int GetLevel()
    {
        return level;
    }   

    public int GetFreeStatPoints()
    {
        return freeStatPoints;
    }

    // Take damage and logic for armor
    // if armor is 100% then first damage decrease armor by 20%
    // if armor is 0% then damage goes directly to health
    public void TakeDamage(int damage)
    {

        m_animator.SetTrigger("Hurt");

        if(currentArmor >= damage){
            currentArmor -= damage;
        }else{
            currentHealth -= damage - currentArmor;
            currentArmor = 0;
        }

        if(tag != "Player"){
            healthBar.SetHealthBar(maxHealth, currentHealth);
            healthBar.SetArmorBar(maxArmor, currentArmor);
            GetComponent<NPCBehaviour>().DisplayPopUpText(damage,"damage");
        }   

        // animatorHandler.PlayTargetAnimation("Damage01",true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            m_animator.SetTrigger("Death");
            
            //disable the collider so the player can walk over the enemy
            GetComponent<CapsuleCollider2D>().enabled = false;
            //fix the position of the enemy so it doesn't fall through the ground
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

            Destroy(gameObject, destroySelfTime);

        }
        else
        {
            // get the player and play the damage sound
            /*      player
                      .GetComponent<PlayerMovementScript>()
                      .PlayDamageSound();*/
        }

    }


    //while the enemy is dead but still visible, the enemy sprite is blinking 
    private void Blink()
    {
        blinkDelta += Time.deltaTime;

        if (isDead && blinkDelta >= blinkDeltaTime)
        {
            if (isVisible)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                isVisible = false;
                blinkDeltaTime -= 0.03f;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                isVisible = true;
            }

            blinkDelta = 0.0f;

        }
    }

    public int  CalculateXpToNextLevel(int _level)
    {
        return  _level * _level + _level;
    }

    public void IncreaseExperience(int amount)
    {
        XP += amount;
        if(XP >= XPToNextLevel){
            int tempXP = XP;
            while(tempXP >= XPToNextLevel){
                tempXP -= XPToNextLevel;
                XP -= XPToNextLevel;
                IncreaseLevel();
            }
        }
    }

    public void IncreaseLevel(){
        level+=1;
        XPToNextLevel = CalculateXpToNextLevel(level);
        SetMaxHealthFromHealthLevel(healthLevel);
        IncreaseFreeStatPoints();

        //play level up animation (enable particle emitter)
        //Get LevelUpParticleEmitter by name
        LevelUpParticleEmitter.SetActive(true);
        Invoke("DisableLevelUpParticle", 1.0f);
    }
    public void DisableLevelUpParticle()
    {
        eventLog.AddEvent(new EventLogItem("Level Up!", Time.time));
        LevelUpParticleEmitter.SetActive(false);

    }

    public void IncreaseFreeStatPoints(){
        freeStatPoints+=1;
    }

    public int GetWeaponDamage()
    {
        //returns weapon_damage + damageLevel*damage_per_level
        return GetComponent<HeroKnight>().m_PlayerActiveWeapon.GetComponent<WeaponScript>().attackDamage;
    }

    public int GetAttackValue(){
        //returns weapon_damage + damageLevel*damage_per_level
        return damageLevel * damageIncreasePerLevel+level;
    }

    public int GetAttackValueFromDamageLevel(int _damageLevel){
        //returns weapon_damage + damageLevel*damage_per_level
        return GetWeaponDamage() + _damageLevel * damageIncreasePerLevel+level;
    }

    public float GetCritChance(){
        return defaultCritChancePct;
    }


// - Stats -
    public void IncreaseHealthLevel()
    {
        healthLevel++;
        maxHealth = SetMaxHealthFromHealthLevel(healthLevel);
        currentHealth = maxHealth;
        DecreaseFreeStatPoints();
    }
    public void DecreaseFreeStatPoints(){
        if(freeStatPoints > 0){
            freeStatPoints-=1;
        }
    }
    public void IncreasePowerLevel(){
        powerLevel++;
        currentPower = maxPower;
        DecreaseFreeStatPoints();
    }
    public void IncreaseStaminaLevel(){
        staminaLevel++;
        stamina = maxStamina;
        DecreaseFreeStatPoints();
    }
    public void IncreaseDamageLevel(){
        damageLevel++;
        attackDamage = damageLevel * 10;
        DecreaseFreeStatPoints();
    }
    //TODO create speed stat(movement speed, attack speed, attack range etc )
    public void IncreaseDexLevel(){
        dexLevel++;
        movementSpeed = dexLevel * 0.5f;
        DecreaseFreeStatPoints();
        //jumpForce = dexLevel * 0.5f;
        //attackRange = dexLevel * 0.5f;
        //attackCooldown = dexLevel * 0.5f;
    }
    public void IncreaseArmorLevel(){
        armorLevel++;
        maxArmor = SetMaxArmorFromArmorLevel();
        DecreaseFreeStatPoints();
        
    }


    public int GetDamageLevel()
    {
        return damageLevel;
    }

}
