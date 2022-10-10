using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    //Get the Player
    public GameObject Player;
    //To get the Player MovementController Script
    MovementController PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        //Getting the Script
        PlayerController = Player.GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if the object colliding with the trigger has Player tag
        {
            PlayerController.inBush = true; //Change inBush value from player to true
            Debug.Log("On Bush: " + PlayerController.inBush);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if it is no longer on bush
        {
            PlayerController.inBush = false; //Change inBush value from player to false
            Debug.Log("On Bush: " + PlayerController.inBush);
        }
    }
}
