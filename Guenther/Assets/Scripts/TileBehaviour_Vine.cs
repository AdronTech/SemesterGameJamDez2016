using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Vine : MonoBehaviour, ITile {

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
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                RaycastHit2D upperBounding = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, 1f);
                Debug.DrawRay(transform.position, Vector3.up);
                if (!upperBounding) {
                    Instantiate(Vine, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.Euler(0, 0, 0));
                }
                break;
            case State.Burning:
                if (gameObject.tag != "Vine")
                    Destroy(gameObject);
                break;
            case State.None:
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
            default:
                break;
        }
    }

    public void Init()
    {
        throw new NotImplementedException();
    }
}
