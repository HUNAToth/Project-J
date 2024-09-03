
using UnityEngine;

//Ez az osztály felelős a járőröző ellenségért, a leftEdge és a RightEdge, engineben megadott paraméterek között
public class EnemyPatrol : MonoBehaviour
{
    //Itt adjuk meg a két szélső pontot amik között járőrőzni fog az enemy
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    //Itt adjuk meg paraméterként, hogy melyik enemy legyen a járőr
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    //speed: mozgási sebesség, 
    //initScale: ez tárolja hogy éppen merre "néz" a karakter, 
    //moveLeft: ez alapján mozog a kaater jobbra vagy balra
    [Header("Movement parameters")]
    private Vector3 initScale;
    private bool moveLeft;

    //Természetesebb viselkedés, ha a járőr a végpontot elérve nem rögtön fordul meg, hanem várakozik egy keveset.
    //Ezért a idleDuration-nal tudjuk megadni mennyi legyen a várakozási idő.
    //Az idleTimer az a timer amely a várakozásért felelős.
    [Header("Idle settings")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    //Paraméterként itt tudjuk megadni mely animátort használja az osztály.
    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
        idleDuration += Random.Range(0.0f, 2.0f);
    }

    private void OnDisable()
    {
        //anim.SetBool("isMove", false);
    }

    private void Update()
    {


        if (enemy.GetComponent<NPCBehaviour>().isPatrolling){

            anim.SetBool("Grounded", true);
            anim.SetInteger("AnimState", 1);
            //ha balra megy éppen a járőr
            if (moveLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                {
                    MoveInDirection(-1);

                }
                else
                {
                    ChangeDirection();
                }
                //ha jobbra megy éppen a járőr
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    ChangeDirection();
                }
            }

        }
    }

    //Irányváltás
    private void ChangeDirection()
    {
        //Ha az enemy elérte a RightEdge/LefteEdge pontot, akkor megáll és várakozik
        // anim.SetBool("isMove", false);

        anim.SetInteger("AnimState", 0);
        idleTimer += Time.deltaTime;
        //Az idleDuration paraméterrel tudjuk megadni a várakozás idejét
        //Ha eltelt a várakozási idő akkor megtörténik az irány változtatás 
        if (idleTimer > idleDuration)
        {
            moveLeft = !moveLeft;
        }

    }

    //Mozgás
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        //anim.SetBool("isMove", true);
        //Enemy sprite képének a forgatása a megfelelő irányba
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Enemy mozgatása a megfelelő irányba
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * GetComponent<CharacterStats>().movementSpeed,
         enemy.position.y, enemy.position.z);

    }
}
