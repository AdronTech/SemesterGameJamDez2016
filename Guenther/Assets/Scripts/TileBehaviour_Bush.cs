using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Bush : MonoBehaviour {

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
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case State.None:
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
            default:
                break;
        }
    }
}