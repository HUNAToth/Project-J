
using UnityEngine;

//Ez az osztály felelős az enemy üldözéséért
public class EnemyChase : MonoBehaviour
{
    //Itt adjuk meg paraméterként, hogy melyik enemy legyen a járőr
    [Header("Enemy")]
    //selft
    [SerializeField] private Transform enemy;
    //target
    [SerializeField] private Transform player;

    //speed: mozgási sebesség, 
    //initScale: ez tárolja hogy éppen merre "néz" a karakter, 
    [Header("Movement parameters")]
    private Vector3 initScale;

   

    //Paraméterként itt tudjuk megadni mely animátort használja az osztály.
    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        //anim.SetBool("isMove", false);
    }

    private void Update()
    {
        if(GetComponent<NPCBehaviour>().target != null){
            player = GetComponent<NPCBehaviour>().target.transform;
        }
        if (enemy.GetComponent<NPCBehaviour>().isChasing){

            anim.SetBool("Grounded", true);
            anim.SetInteger("AnimState", 1);
            //ha balra megy éppen a járőr
            if (enemy.position.x >= player.transform.position.x)
            {
                MoveInDirection(-1);

            }else
            {
                MoveInDirection(1);

            }
        }
    }


    //Mozgás
    private void MoveInDirection(int _direction)
    {

        //Enemy sprite képének a forgatása a megfelelő irányba
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Enemy mozgatása a megfelelő irányba
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * GetComponent<CharacterStats>().chaseSpeed,
         enemy.position.y, enemy.position.z);

    }
}
