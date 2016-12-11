using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Cloud : MonoBehaviour {
    public ParticleSystem ps_rain;
    private int lastSeason = 0;
    private enum State { None, Raining };
    private State curState = State.None;
    private Dictionary<Player.Seasons, State> states = new Dictionary<Player.Seasons, State>();

    void Awake() {
        states.Add(Player.Seasons.Spring, State.Raining);
        states.Add(Player.Seasons.Summer, State.None);
        states.Add(Player.Seasons.Autumn, State.Raining);
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
            case State.Raining:
                ps_rain.Play();
                GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case State.None:
                GetComponent<SpriteRenderer>().enabled = false;
                ps_rain.Stop();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
            default:
                break;
        }
    }
}