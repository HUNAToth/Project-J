using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject GreenHealthBar;
    public GameObject RedHealthBar_Green_BG;
    public GameObject YellowHealthBar;
    public GameObject RedHealthBar_Yellow_BG;


    private void Awake()
    {
        //turn green HealthBar off then on
        //TODO fix this
       /* YellowHealthBar.SetActive(false);
        YellowHealthBar.SetActive(true);
        GreenHealthBar.SetActive(false);
        GreenHealthBar.SetActive(true);*/
    }

    //sets the "green" bar in percentage of the health (min 0 - max 100)
        public void SetHealthBar(int maxHealth, int health){
            //cast to float to get the percentage
            float f_maxHealth = (float)maxHealth;
            float f_health = (float)health;
            float healthPercentage = f_health / f_maxHealth;

            //Debug.Log("SetHealthBar() ran, current health percentage" + ( healthPercentage));
            if(health <= 0){
                GreenHealthBar.transform.localScale = new Vector3(0.0f, 1f);
                GreenHealthBar.transform.position = new Vector3(0.0f, 1f);
            }else{
                GreenHealthBar.transform.localScale = 
                new Vector3( 1.0f*(healthPercentage), 1f);
            }

            if(health == 0){
                Destroy(GreenHealthBar);
                Destroy(RedHealthBar_Green_BG);
            }
        }
        public void SetArmorBar(int maxArmor, int armor){
            if(armor>0){
                //cast to float to get the percentage
                float f_maxArmor = (float)maxArmor;
                float f_armor = (float)armor;
                float armorPercentage = f_armor / f_maxArmor;

                if(f_armor <= 0){
                    YellowHealthBar.transform.localScale = new Vector3(0.0f, 1f);
                    YellowHealthBar.transform.position = new Vector3(0.0f, 1f);
                }else{
                    YellowHealthBar.transform.localScale = 
                    new Vector3( 1.0f*(armorPercentage), 1f);
                }
            }
            if(armor == 0){
                Destroy(YellowHealthBar);
                Destroy(RedHealthBar_Yellow_BG);
            }
        }
}
