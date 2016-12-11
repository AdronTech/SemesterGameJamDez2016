using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    private Animator anim;
    public enum Seasons { Spring, Summer, Autumn, Winter };
    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    public static Seasons actualSeason;
    public static bool death;
    private bool ClimbInVine;
    private bool faceToRight = true;
    private float sidewaysInput;
    private float jumpInput;
    private GameObject player;
    private Vector3 velocity;
    private int appleCount;
   


    private void Awake()
    {
        sidewaysInput = jumpInput = 0;
        player = this.gameObject;
        actualSeason = Seasons.Spring;
        appleCount = 0;
    }

    void Start() {
        anim = this.GetComponent<Animator>();
     
    }

    private void Update()
    {
        GetPlayerInput();
        Run();
        Jump();
    }

    private void GetPlayerInput()
    {
        sidewaysInput = Input.GetAxis(inputSettings.PLAYER_SIDEWAYS_AXIS);
        jumpInput = Input.GetAxisRaw(inputSettings.PLAYER_JUMP_AXIS);

        // change the season
        if (Input.GetButtonDown("Jump"))
        {
            actualSeason++;
            if ((int)actualSeason == System.Enum.GetValues(typeof(Seasons)).Length)
            {
                actualSeason = Seasons.Spring;
            }

        }
        if (ClimbInVine && jumpInput != 0)
        {
            transform.Translate(Vector2.up * jumpInput * 0.1f);
        }
        if (actualSeason == Seasons.Summer) {
            Physics2D.gravity = new Vector2(0f, -9.81f);
            ClimbInVine = false;
        }
    }

    private void Run()
    {

        velocity = new Vector3(sidewaysInput * moveSettings.RunVelocity * 10.0f, player.GetComponent<Rigidbody2D>().velocity.y);
        bool TileHit = false;
        if (velocity.x < 0)//moving to left
        {
            RaycastHit2D hitL1 = Physics2D.Raycast(player.transform.position + new Vector3(0.0f, 0.95f), Vector3.left, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitL2 = Physics2D.Raycast(player.transform.position, Vector3.left, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitL3 = Physics2D.Raycast(player.transform.position - new Vector3(0.0f, 0.95f), Vector3.left, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            if (hitL1 || hitL2 || hitL3) TileHit = true;
            if (faceToRight) {
                faceToRight = false;
                gameObject.transform.localScale = new Vector3(-1f * transform.localScale.x,transform.lossyScale.y,transform.lossyScale.z);
            }
        }
        else if (velocity.x > 0) //moving to right 
        {
            RaycastHit2D hitR1 = Physics2D.Raycast(player.transform.position + new Vector3(0.0f, 0.9f), Vector3.right, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitR2 = Physics2D.Raycast(player.transform.position, Vector3.right, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitR3 = Physics2D.Raycast(player.transform.position - new Vector3(0.0f, 0.9f), Vector3.right, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            if (hitR1 || hitR2 || hitR3) TileHit = true;
            if (!faceToRight) {
                faceToRight = true;
                gameObject.transform.localScale = new Vector3(-1f * transform.localScale.x, transform.lossyScale.y, transform.lossyScale.z);
            }
        }

        if (!TileHit)
        {
            player.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(velocity);
        }
        else
        {
            velocity = new Vector3(0, player.GetComponent<Rigidbody2D>().velocity.y);

            player.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(velocity);
        }
        //player.GetComponent<Rigidbody2D>().MovePosition(transform.position + new Vector3(sidewaysInput * 0.1f * moveSettings.RunVelocity, 0));
    }

    private void Jump()
    {
        if (jumpInput > 0 && playerGrounded())
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(player.GetComponent<Rigidbody2D>().velocity.x, moveSettings.JumpVelocity, velocity.z);
        }
    }

    private bool playerGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position - new Vector3(0.3f, 0.0f), Vector3.down, moveSettings.DistanceToGround, moveSettings.Ground);
        Debug.DrawRay(player.transform.position, Vector3.down, Color.red, 1f);
        RaycastHit2D hit2 = Physics2D.Raycast(player.transform.position + new Vector3(0.3f, 0.0f), Vector3.down, moveSettings.DistanceToGround, moveSettings.Ground);
        Debug.DrawRay(player.transform.position, Vector3.down, Color.red, 1f);
        if (hit || hit2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        death = other.gameObject.tag == "death";

        //hier stirbt er 
        if(death)
        anim.SetBool("isDead", true);
        Debug.Log(anim.GetBool("isDead"));

        if (other.gameObject.tag == "apple")
        {
            appleCount++;
            AppleManager.Instance.Applenr++;
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<TileBehaviour_Vine>())
        {
            Physics2D.gravity = new Vector2(0f, 0f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2();
            ClimbInVine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TileBehaviour_Vine>())
        {
            Physics2D.gravity = new Vector2(0f, -9.81f);
            ClimbInVine = false;
        }
    }

    [System.Serializable]
    public class MoveSettings
    {
        public float RunVelocity = 0.4f;
        public float JumpVelocity = 6.5f;
        public float DistanceToGround = 1.03f;
        public float DistanceToBlockingTile = 0.53f;
        public LayerMask Ground;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string PLAYER_SIDEWAYS_AXIS = "Horizontal";
        public string PLAYER_JUMP_AXIS = "Vertical";
        public string PLAYER_SEASON_CHANGE = "Jump";
    }
}
