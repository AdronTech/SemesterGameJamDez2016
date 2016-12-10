using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Bush : MonoBehaviour {

    public Material leaf, fire;

    private int lastSeason = 0;
    private enum State { None, Burning };
    private State curState = State.None;
    private Dictionary<Player.Seasons, State> states = new Dictionary<Player.Seasons, State>();
    private ParticleSystem ps;

    void Awake() {
    }

    void Start()
    {
        StartCoroutine(TileLife());
    }

    IEnumerator TileLife()
    {
        while(true)
        {
            switch(Player.actualSeason)
            {
                case Player.Seasons.Summer:
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    break;
                case Player.Seasons.Spring:
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                case Player.Seasons.Autumn:
                case Player.Seasons.Winter:
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    break;
            }

            yield return 0;

        }
    }
    private void changeState() {
        switch (curState) {
            case State.Burning:
                
                break;
            case State.None:
                
                break;
            default:
                break;
        }
    }
}