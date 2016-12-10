using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Vine : MonoBehaviour {

    public GameObject Vine;
    private int lastSeason = 0;
    private enum State { None, Growing, Burning };
    private State curState = State.None;
    private Dictionary<Player.Seasons, State> states = new Dictionary<Player.Seasons, State>();

    void Awake() {
        states.Add(Player.Seasons.Spring, State.Growing);
        states.Add(Player.Seasons.Summer, State.Burning);
        states.Add(Player.Seasons.Autumn, State.None);
        states.Add(Player.Seasons.Winter, State.None);
    }

    void Start() {
        curState = states[Player.actualSeason];
        changeState();
    }

    // Use this for initialization
    void Update() {
        if (lastSeason != (int)Player.actualSeason) {
            curState = states[Player.actualSeason];
            changeState();
        }
        lastSeason = (int)Player.actualSeason;
    }

    private void changeState() {
        switch (curState) {
            case State.Growing:
                RaycastHit2D upperBounding = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.75f), Vector2.up, 0.2f);
                Debug.DrawRay(transform.position, Vector3.up);
                if (!upperBounding) {
                    Instantiate(Vine, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.Euler(0, 0, 0));
                }
                break;
            case State.Burning:
                
                break;
            case State.None:
                break;
            default:
                break;
        }
    }
}
