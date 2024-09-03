using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

public int attackDamage = 10;
public float attackCoolDown = 0.25f;
public int staminaCost = 10;

public float attackRange = 1.0f;
public float critChancePct = 10.0f;
public float critDamageMultiplier = 2.0f;
public CharacterStats OwnerStats;

public EventLog eventLog;


    void Awake(){
        OwnerStats = GetComponentInParent<CharacterStats>();
        attackCoolDown += Random.Range(0.0f, 0.25f);
        eventLog = GameObject.Find("EventLog").GetComponent<EventLog>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Component CharacterStats = other.gameObject.GetComponent("CharacterStats");
        if (other.gameObject.tag=="Enemy"||other.gameObject.tag=="Player")
        {
            int finalDamage = attackDamage + OwnerStats.GetAttackValue();
            if (Random.Range(0.0f, 100.0f) <= critChancePct+OwnerStats.GetCritChance())
            {
                finalDamage *= (int)critDamageMultiplier;
            }
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(finalDamage);
            eventLog.AddEvent(new EventLogItem("Hit " + other.gameObject.name + " for " + finalDamage + " damage", Time.time));
        }
    }
}
