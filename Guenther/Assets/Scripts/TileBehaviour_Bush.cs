using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Bush : MonoBehaviour {

    private bool onFire = false;
    private int lastSeason = 0;
    private enum State { None, Burning };
    private State curState = State.None;
    private Dictionary<Player.Seasons, State> states = new Dictionary<Player.Seasons, State>();

    void Awake() {
        states.Add(Player.Seasons.Spring, State.None);
        states.Add(Player.Seasons.Summer, State.Burning);
        states.Add(Player.Seasons.Autumn, State.None);
        states.Add(Player.Seasons.Winter, State.None);
    }

    void Start() {
        curState = states[Player.actualSeason];
        changeState();
    }

    void Update() {
        if (lastSeason != (int)Player.actualSeason) {
            curState = states[Player.actualSeason];
            changeState();
        }
        lastSeason = (int)Player.actualSeason;
    }

    private void changeState() {
        switch (curState) {
            case State.Burning:
                onFire = true;
                break;
            case State.None:
                onFire = false;
                break;
            default:
                break;
        }
    }
    void OnTriggerEnter2D (Collider2D other) {
        Debug.Log("Test");
        if (onFire && other.tag == "Player") {
            other.GetComponent<Player>().Spawn();
        }
    }
}