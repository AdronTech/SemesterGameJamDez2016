using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Corn : MonoBehaviour {

    public GameObject Corn;
    private int lastSeason = 0;
    private enum State { Low, Mid, High, None };
    private State curState = State.None;
    private Dictionary<Player.Seasons, State> states = new Dictionary<Player.Seasons, State>();

    void Awake() {
        states.Add(Player.Seasons.Spring, State.Mid);
        states.Add(Player.Seasons.Summer, State.High);
        states.Add(Player.Seasons.Autumn, State.None);
        states.Add(Player.Seasons.Winter, State.Low);
    }

    // Use this for initialization
    void Start() {
        curState = states[Player.actualSeason];
        changeState();
    }

    // Update is called once per frame
    void Update() {
        if (lastSeason != (int)Player.actualSeason) {
            curState = states[Player.actualSeason];
            changeState();
        }
        lastSeason = (int)Player.actualSeason;
    }

    private void changeState() {
        switch (curState) {
            case State.Low:
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case State.Mid:
                RaycastHit2D upperBounding = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, 1f);
                if (!upperBounding && gameObject.tag == "Corn") {
                    Instantiate(Corn, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.Euler(0, 0, 0));
                }
                break;
            case State.High:
                RaycastHit2D upperBoundingTwice = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.up, 1f);
                if (!upperBoundingTwice && gameObject.tag == "Corn") {
                    Instantiate(Corn, new Vector2(transform.position.x, transform.position.y + 2f), Quaternion.Euler(0, 0, 0));
                }
                break;
            case State.None:
                if (gameObject.tag != "Corn")
                    Destroy(gameObject);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
            default:
                break;
        }
    }
}
