using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUpFactory : MonoBehaviour {
    [SerializeField]
    public PowerUp DoubleJumpPrefab;

    
   /* public static GameObject CreateRandomPowerUp(int level){
        //create a prefab, and then instantiate it

    
        Vector3 position = new Vector3(0,0,0);
        position.x += Random.Range(-2f, 2f);
        position.z += Random.Range(-2f, 2f);
        GameObject item =  GameObject.Instantiate(DoubleJumpPrefab, position, Quaternion.identity);

        //get a random power up name
        string powerUpName = GetRandomPowerUpName();
        item.GetComponent<PowerUp>().powerUpName = powerUpName;
        //get a random amount by level
        item.GetComponent<PowerUp>().amount = Random.Range( level*level-level+1, level * level);

        return item;
   }*/
   public static string GetRandomPowerUpName(){
         string[] powerUpNames = new string[8]{"DoubleJump", "WallGrab", "HealthBonus", "StaminaBonus", "GoldBonus","PowerBonus", "XPBonus", "ArmorBonus" };
         return powerUpNames[Random.Range(0, powerUpNames.Length)];
   }
}
