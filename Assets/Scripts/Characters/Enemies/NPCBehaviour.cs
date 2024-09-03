using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject target = null;
    public CharacterStats selfStats;
    public CharacterStats targetStats;

    public bool isEnemy = false;

    public bool doesPatrol = false;
    public bool doesChase = false;
    public bool doesMeleeAttack = false;
    public bool doesRangedAttack = false;

    public bool dropLoot = false;
    public bool canDie  = true;
    public bool isPatrolling = false;
    public bool isChasing = false;
    public bool isMeleeAttacking = false;
    public bool isRangedAttacking = false;

    public GameObject PopUpTextObject;

    //TODO create CharacterTeam enum file, now its in CharacterStats.cs too
    public CharacterTeam[] enemyTeams;

    public bool CompareTeams(CharacterStats.CharacterTeam team1, CharacterTeam team2){
        if(team1.ToString() == team2.ToString()){
            return true;
        }else{
            return false;
        }
        
    }
    private void InitEnemyTeamArray(){
        switch(selfStats.selfTeam){
            case CharacterStats.CharacterTeam.Player:
                enemyTeams = new CharacterTeam[]{CharacterTeam.Enemy};
                break;
            case CharacterStats.CharacterTeam.Enemy:
                enemyTeams = new CharacterTeam[]{CharacterTeam.Player, CharacterTeam.Enemy2};
                break;
            case CharacterStats.CharacterTeam.Enemy2:
                enemyTeams = new CharacterTeam[]{CharacterTeam.Player, CharacterTeam.Enemy};
                break;
            case CharacterStats.CharacterTeam.Neutral:
                enemyTeams = new CharacterTeam[]{};
                break;
            default:
                enemyTeams = new CharacterTeam[]{CharacterTeam.Player, CharacterTeam.Enemy };
                break;
        }
    }


    public void Start()
    {
        selfStats = GetComponent<CharacterStats>();
        InitEnemyTeamArray();
            
    }

    public void Update(){
        if(selfStats.GetCurrentHealth()>0)
        {
        //if target is not in sight, patrol
        if( target==null && doesPatrol) {
            Patrol();
        }

        SearchForTarget();
       
        //if player is in sight, chase
        if ( doesChase &&
            target != null &&
            Vector3.Distance(transform.position, target.transform.position) <= selfStats.sightRange)
        {
            Chase();
        }
        //if player is in attack range, attack
        if ( 
            (doesMeleeAttack || doesRangedAttack) &&
            target != null && 
            Vector3.Distance(transform.position, target.transform.position) <= selfStats.attackRange){
                Attack();
        }
        }else{
            //if the enemy is dead, stop all behaviour
            isPatrolling = false;
            isChasing = false;
            isMeleeAttacking = false;
            isRangedAttacking = false;
            if(dropLoot){
                DropLoot();
            }
        }
    }

    public void Attack(){
        isPatrolling = false;
        isChasing = false;
        isMeleeAttacking = true;
    }

//the behaviour is controlled by EnemyPatrol.cs
    public void Patrol(){
        isPatrolling = true;
        isChasing = false;
        isMeleeAttacking = false;
    }

//the behaviour is controlled by EnemyChase.cs
    public void Chase(){
        isChasing = true;
        isPatrolling = false;
        isMeleeAttacking = false;   
    }

    public void SetTarget(GameObject newTarget){
        target = newTarget;
    }

    public void DropLoot(){
        if(dropLoot){
            //create loot

               foreach (ObjectEmitter emitter in GetComponentsInChildren<ObjectEmitter>())
               {
                   emitter.Emit();
               }
            }
        }



    public void DisplayPopUpText(int _amount, string _type="damage")
    {
        //on damage/heal, create a text(TMP) object
        //the text will be red for damage, green for heal
        //the text will be randomised in position and rotation
        //the text will be randomised in size but will be bigger for bigger numbers
        //the text will slowly fade out, and will be destroyed after 1,5 second
        GameObject popUpText = PopUpTextObject;

        if (popUpText == null || !GameObject.Find("Canvas"))
        {
            Debug.LogError("PopUpTextObject or Canvas not assigned.");
            return;
        }

        popUpText.transform.SetParent(transform);
  
        popUpText.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0.5f, 1.0f), 0);
        popUpText.transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-20, 20));
        if (transform.localScale.x < 0)
        {
            popUpText.transform.localScale = new Vector3(-1, 1, 1) * _amount / 10;
        }
        else
        {
            popUpText.transform.localScale = new Vector3(1, 1, 1) * _amount / 10;
        }
        TMPro.TextMeshPro textMeshComp = popUpText.GetComponent<TMPro.TextMeshPro>();

        if (textMeshComp == null){
            Debug.LogError("PopUpTextObject does not have a TextMeshPro component.");
            return;
        }
        textMeshComp.text = _amount.ToString();
        if (_type == "damage")
        {
            textMeshComp.color = new Color(1, 0, 0, 1);
        }
        else if (_type == "heal")
        {
            textMeshComp.color = new Color(0, 1, 0, 1);
        }

        //scale the text down over time
        StartCoroutine(ScaleTextOverTime(popUpText, 0.75f));
    }

    IEnumerator ScaleTextOverTime(GameObject _textObject, float _time)
    {
        float elapsedTime = 0;
        Vector3 startingScale = _textObject.transform.localScale;
        Vector3 targetScale = new Vector3(0, 0, 0);
        while (elapsedTime < _time)
        {
            _textObject.transform.localScale = Vector3.Lerp(startingScale, targetScale, (elapsedTime / _time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDestroy() {
            
    }
    private void SearchForTarget(){
        //look for targets, in sight range, on 'Character' layer, ignoring self
        //the closest target in sight range will be the target
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, selfStats.sightRange, LayerMask.GetMask("Character"));
        Collider2D[] filteredTargets = Array.FindAll(
                                                        targets,
                                                        x => x.gameObject != this.gameObject
                                                        && Array.Exists(enemyTeams, element => CompareTeams(x.gameObject.GetComponent<CharacterStats>().selfTeam, element))
                                                    );
        float closestDistance = Mathf.Infinity;
        if (filteredTargets.Length == 0)
        {
            target = null;
        }
        foreach (Collider2D target in filteredTargets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                this.target = target.gameObject;
            }
        }
    }
}





