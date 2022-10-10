using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
   // private new SpriteRenderer spriteRenderer;
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    //Needed to know if it is Moving
    public bool isMoving = false;
    //A Vector used to see if there is any input to move X and Y will always be -1,0 or 1
    //and each tile is 1x1 so the distance will always be 1 tile
    Vector2 movement;
    //To check if it is still in bush for a random fight to happen, this bool is changed on the Bush script.
    public bool inBush = false;

    public bool canMove = true; // Changed from original script to fit lab

    //To enter battle screen
    public GameObject battleS;

    private void Awake()
    {
       rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (battleS.gameObject.activeSelf == false) // to avoid movement while fighting
        {
            if (!isMoving)
            {
                //Check if there is any movement
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                //No diagonal movement
                if (movement.x != 0)
                {
                    movement.y = 0;
                }

                //Start Movement
                if (canMove == true)
                {
                    if (movement != Vector2.zero)
                    {
                        var targetPos = transform.position; //Makes a vector with targer position
                        targetPos.x += movement.x; //Add either 1 or -1 to X which is the tile on top or down
                        targetPos.y += movement.y; //Same but with Y either left or right

                        StartCoroutine(Move(targetPos)); //Start Move Coroutine for tile base movement
                    }
                }
            }
        }
    }

    //Moving position to a new direction
    private void FixedUpdate()
    {

    }

    IEnumerator Move(Vector3 tPos)
    {
        Vector3 sPos = transform.position;

        isMoving = true; //Now it is moving

        //Will move towards the new position until the diference in distance is
        //less than Epsilon (The smallest value that a float can have different from zero.)
        while ((tPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {

            transform.position = Vector3.MoveTowards(transform.position, tPos, speed * Time.deltaTime);

            Debug.Log("Is Moving " + isMoving);

            yield return null;
        }

        //Once it reaches now that is the new position for the character
        transform.position = tPos;
        //Is no longer moving
        isMoving = false;

        //If after moving the Character is still in a bush will be chances to start a battle
        if (inBush == true)
        {
            float Chance = Random.value; // a random number between 0 and 1.0
            Debug.Log("Random: " + Chance);
            if (Chance < 0.1) // a 10% chance
            {
                Debug.Log("BATTLE");
                battleS.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("AVOID");
            }
        }

    }
}
