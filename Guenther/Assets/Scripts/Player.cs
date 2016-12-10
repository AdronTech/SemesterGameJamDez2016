using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    public enum Seasons { Spring, Summer, Autumn, Winter };
    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    public static Seasons actualSeason;
    public static bool death;
    private float sidewaysInput;
    private float jumpInput;
    private GameObject player;
    private Vector3 velocity;

    private void Awake()
    {
        sidewaysInput = jumpInput = 0;
        player = this.gameObject;
        actualSeason = Seasons.Spring;
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
            Debug.Log(actualSeason);
        }
    }

    private void Run()
    {

        velocity = new Vector3(sidewaysInput * moveSettings.RunVelocity * 10.0f, player.GetComponent<Rigidbody2D>().velocity.y);
        bool TileHit = false;
        if (velocity.x < 0)//moving to right
        {
            RaycastHit2D hitL1 = Physics2D.Raycast(player.transform.position + new Vector3(0.0f, 0.9f), Vector3.left, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitL2 = Physics2D.Raycast(player.transform.position, Vector3.left, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitL3 = Physics2D.Raycast(player.transform.position - new Vector3(0.0f, 0.9f), Vector3.left, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            if (hitL1 || hitL2 || hitL3) TileHit = true;
        }
        else if (velocity.x > 0) //moving to right 
        {
            RaycastHit2D hitR1 = Physics2D.Raycast(player.transform.position + new Vector3(0.0f, 0.9f), Vector3.right, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitR2 = Physics2D.Raycast(player.transform.position, Vector3.right, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            RaycastHit2D hitR3 = Physics2D.Raycast(player.transform.position - new Vector3(0.0f, 0.9f), Vector3.right, moveSettings.DistanceToBlockingTile, moveSettings.Ground);
            if (hitR1 || hitR2 || hitR3) TileHit = true;
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
