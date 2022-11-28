using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    public float speedV = 5f;

    public Animator Animations;

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
    public Bush selectedBush;

    public Fader fader;
    public AudioManager audioManager;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //if(Singleton.Instance.Load == true)
        //{
        //    foreach (string line in System.IO.File.ReadLines(@"..\GAME3023-Lab-3\SaveData.txt"))
        //    {
        //        string[] csv = line.Split(" ");
        //        transform.position = new Vector3(float.Parse(csv[0]), float.Parse(csv[1]), 0);
        //    }
        //}

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

                if(movement.x > 0)
                {
                    Animations.SetInteger("Direction", 1);
                }
                else if(movement.x < 0)
                {
                    Animations.SetInteger("Direction", 3);
                }
                else if (movement.y > 0)
                {
                    Animations.SetInteger("Direction", 0);
                }
                else if (movement.y < 0)
                {
                    Animations.SetInteger("Direction", 2);
                }

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

    public void Save()
    {
        using (StreamWriter sw = new StreamWriter("SaveData.txt"))
        {
            string x = transform.position.x.ToString();
            string y = transform.position.y.ToString();

            string data = x + " " + y;
            sw.WriteLine(data); 

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bush") //if the object colliding with the trigger has Player tag
        {
            selectedBush = collision.gameObject.GetComponent<Bush>();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bush") //if the object colliding with the trigger has Player tag
        {
            selectedBush = null;

        }
    }

    IEnumerator Move(Vector3 tPos)
    {
        Vector3 sPos = transform.position;

        isMoving = true; //Now it is moving
        Animations.SetBool("isMoving", true);

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
        Animations.SetBool("isMoving", false);

        //If after moving the Character is still in a bush will be chances to start a battle
        if (inBush == true)
        {
            float Chance = Random.value; // a random number between 0 and 1.0
            Debug.Log("Random: " + Chance);
            if (Chance < 0.1) // a 10% chance
            {
                ToBattle();
                //battleS.gameObject.SetActive(true);
                //selectedBush.Encounter();

            }
            else
            {
                Debug.Log("AVOID");
            }
        }

    }

    public IEnumerator GoToBattle()
    {
        yield return new WaitForSeconds(0.4f);
        battleS.gameObject.SetActive(true);
        selectedBush.Encounter();
        fader.fadeOut();
    }

    public void ToBattle()
    {
        speed = 0;
        fader.fadeIn();
        audioManager.CrossFadeTO(AudioManager.TrackID.inTown);
        StartCoroutine(GoToBattle());
    }

    public void GoToCave()
    {

        fader.fadeIn();
        fader.StartCoroutine(fader.GoToCaveCoro());
    }

    public void GoToTown()
    {

        fader.fadeIn();
        fader.StartCoroutine(fader.GoToTownCoro());
    }

    public void Flee() ////TEST
    {
        battleS.gameObject.SetActive(false);
        audioManager.CrossFadeTO(AudioManager.TrackID.inCave);
        speed = speedV;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CaveNtrance"))
        {
            GoToCave();
        }

        if (other.CompareTag("CaveExit"))
        {
            GoToTown();
        }
    }
}
