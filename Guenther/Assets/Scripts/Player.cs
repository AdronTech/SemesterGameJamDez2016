using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    public enum Seasons { Spring, Summer, Autumn, Winter };
    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    public static Seasons actualSeason;
    public static bool death;
    private float sidewaysInput;
    private float jumpInput;
    private GameObject player;
    private Vector3 velocity;

    private void Awake() {
        sidewaysInput = jumpInput = 0;
        player = this.gameObject;
        actualSeason = Seasons.Spring;
    }

    private void Update() {
        GetPlayerInput();
        Run();
        Jump();
    }

    private void GetPlayerInput() {
        sidewaysInput = Input.GetAxis(inputSettings.PLAYER_SIDEWAYS_AXIS);
        jumpInput = Input.GetAxisRaw(inputSettings.PLAYER_JUMP_AXIS);

        // change the season
        if (Input.GetButtonDown("Jump")) {
            actualSeason++;
            if ((int)actualSeason == System.Enum.GetValues(typeof(Seasons)).Length) {
                actualSeason = Seasons.Spring;
            }
            Debug.Log(actualSeason);
        }
    }

    private void Run() {
        velocity = new Vector3(sidewaysInput * moveSettings.RunVelocity, player.GetComponent<Rigidbody2D>().velocity.y);
        player.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(velocity);
    }

    private void Jump() {
        if (jumpInput > 0 && playerGrounded()) {
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(player.GetComponent<Rigidbody2D>().velocity.x, moveSettings.JumpVelocity, velocity.z);
        }
    }

    private bool playerGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.down, moveSettings.DistanceToGround, moveSettings.Ground);
        Debug.DrawRay(player.transform.position, Vector3.down, Color.red, 1f);
        if (hit) {
            return true;
        } else {
            return false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        death = other.gameObject.tag == "death";
    }

    [System.Serializable]
    public class MoveSettings {
        public float RunVelocity = 0.8f;
        public float JumpVelocity = 100f;
        public float DistanceToGround = 4f;
        public LayerMask Ground;
    }

    [System.Serializable]
    public class InputSettings {
        public string PLAYER_SIDEWAYS_AXIS = "Horizontal";
        public string PLAYER_JUMP_AXIS = "Vertical";
        public string PLAYER_SEASON_CHANGE = "Jump";
    }
}
