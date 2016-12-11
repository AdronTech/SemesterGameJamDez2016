using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Cloud : MonoBehaviour {
    public ParticleSystem ps_rain;
    public Vector3 pos;
    private int lastSeason = 0;
    private enum State { None, Raining };
    private State curState = State.None;
    private Dictionary<Player.Seasons, State> states = new Dictionary<Player.Seasons, State>();
    private Transform texture;

    void Awake() {
        states.Add(Player.Seasons.Spring, State.Raining);
        states.Add(Player.Seasons.Summer, State.None);
        states.Add(Player.Seasons.Autumn, State.Raining);
        states.Add(Player.Seasons.Winter, State.None);
    }

    void Start() {
        curState = states[Player.actualSeason];
        pos = transform.position;
        texture = transform.FindChild("cloud");
        texture.GetComponent<SpriteRenderer>().enabled = false;
        changeState();
        StartCoroutine(Wiggle());
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
                StartCoroutine(Blink());
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case State.None:
                ps_rain.Stop();
                StartCoroutine(Blink());
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;
            default:
                break;
        }
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < 5; i++)
        {
            texture.GetComponent<SpriteRenderer>().enabled = !texture.GetComponent<SpriteRenderer>().enabled;

            yield return new WaitForSeconds(0.05f); 
        }

        yield break;
    }

    IEnumerator Wiggle()
    {
        float off = 0.2f;

        while (true)
        {
            texture.localPosition = Vector3.Lerp(texture.localPosition, new Vector3(
                Random.Range(off, -off),
                Random.Range(off, -off),
                0), 0.1f);

            yield return 0;
        }
        yield break;
    }

}