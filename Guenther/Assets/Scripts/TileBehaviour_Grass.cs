using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Grass : MonoBehaviour {

    private static Sprite grass;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        if (grass == null)
            grass = Resources.Load<Sprite>("Textures/grass");

        Check();

        StartCoroutine(TileLife());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Check()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);

        if (hit)
        {
            if (hit.collider.GetComponent<TileBehaviour_Base>() != null)
            {
                return;
            }
        }

        GameObject go = new GameObject("Grass");
        go.AddComponent<SpriteRenderer>();
        go.transform.SetParent(transform);
        go.transform.position = transform.position;

        sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = grass;
    }

    IEnumerator TileLife()
    {
        while (true)
        { 

            yield return new WaitUntil(() => Player.actualSeason == Player.Seasons.Winter);
            ChangeColor(Color.white);

            yield return new WaitUntil(() => Player.actualSeason == Player.Seasons.Spring);
            ChangeColor(Color.green);

            yield return new WaitUntil(() => Player.actualSeason == Player.Seasons.Autumn);
            ChangeColor(Color.yellow);

        }
    }

    void ChangeColor(Color c)
    {
        if (sr != null)
            sr.color = c;
    }
}
