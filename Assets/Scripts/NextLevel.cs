using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
     HandleCollision();   
    }

     void HandleCollision(){
       if (Vector3.Distance(transform.position, player.transform.position) < 3.0f)
        {
            //reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
           
        }
    }

    //TODO USE THIS INSTEAD OF THE COLLISION DETECTION
    /* void OnTriggerEnter2D(Collider other) {
        Debug.Log("Collision detected with " + other.gameObject.tag);
        if (other.gameObject.tag=="Player")
        {
            //reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        
        }
    }*/
}
