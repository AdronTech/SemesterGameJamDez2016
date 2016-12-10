using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Grass : MonoBehaviour, ITile {

    public Sprite grass;
    private SpriteRenderer sr;

    void Start()
    {
        Transform grass = transform.FindChild("Grass");
        if (grass != null)
            sr = grass.GetComponent<SpriteRenderer>();

        StartCoroutine(TileLife());
    }

    void ITile.Init()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);

        if (!(hit && hit.collider.GetComponent<TileBehaviour_Base>() != null))
        {
            GameObject go = new GameObject("Grass");
            sr = go.AddComponent<SpriteRenderer>(); 
            go.transform.SetParent(transform);
            go.transform.position = transform.position;

            sr.sortingOrder = transform.childCount;
            sr.sprite = grass;
        }

    }

    IEnumerator TileLife()
    {
        Color c = Color.green;
        while (true)
        {
            switch (Player.actualSeason)
            {
                case Player.Seasons.Winter:
                    c = Color.white;
                    break;
                case Player.Seasons.Spring:
                case Player.Seasons.Summer:
                    c = Color.green;
                    break;
                case Player.Seasons.Autumn:
                    c = Color.yellow;
                    break;
            }

            ChangeColor(c);

            yield return 0;
        }

    }

    void ChangeColor(Color c)
    {
        if (sr != null)
        {
            sr.color = Color.Lerp(sr.color, c, 0.05f);
        }
    }
}
