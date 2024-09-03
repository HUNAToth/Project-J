using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private GameObject weapon;
    //self
    [SerializeField] private GameObject enemy;
    private float timeSinceAttack = 0.0f;
    private int currentAttack = 0;
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {

        if (enemy.GetComponent<NPCBehaviour>().isMeleeAttacking){

            animator.SetInteger("AnimState", 0);
            // Increase timer that controls attack combo
            timeSinceAttack += Time.deltaTime;
            

            //clear attack flag
            if(timeSinceAttack > weapon.GetComponent<WeaponScript>().attackCoolDown)
            {
                ExecuteAttack();
            }

        }
    }

    public void ExecuteAttack(){
        currentAttack++;

        if (currentAttack > 3)
            currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttack > 2.0f)
            currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.SetTrigger("Attack" + currentAttack);
        // Reset timer
        timeSinceAttack = 0.0f;

    }

}
