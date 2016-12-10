using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Water : MonoBehaviour, ITile {

    public Sprite water, water_single, water_left, water_middle, water_right, ice;
    private SpriteRenderer waterRenderer, iceRenderer;

    void Start()
    {
        Transform iceObj = transform.FindChild("Ice");
        if (iceObj != null)
            iceRenderer = iceObj.GetComponent<SpriteRenderer>();

        StartCoroutine(TileLife());
    }

    void ITile.Init()
    {

        GameObject waterGO = new GameObject("Water");
        waterRenderer = waterGO.AddComponent<SpriteRenderer>();
        waterGO.transform.SetParent(transform);
        waterGO.transform.position = transform.position;

        waterRenderer.sortingOrder = transform.childCount;
        waterRenderer.sprite = water;
        waterRenderer.color = new Color(1/255f, 45 / 255f, 83 / 255f);

        GameObject iceGO = new GameObject("Ice");
        iceRenderer = iceGO.AddComponent<SpriteRenderer>();
        iceGO.transform.SetParent(transform);
        iceGO.transform.position = transform.position;
        iceRenderer.sortingOrder = transform.childCount;

        // ice
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);

        if (!(hit && hit.collider.GetComponent<TileBehaviour_Water>() != null))
        {
            iceRenderer.sprite = ice;
        }

        // water
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (!(hit && hit.collider.GetComponent<TileBehaviour_Water>() != null))
        {
            bool left, right;
            RaycastHit2D l = Physics2D.Raycast(transform.position, Vector2.left, 1f);
            RaycastHit2D r = Physics2D.Raycast(transform.position, Vector2.right, 1f);

            left = l && l.collider.GetComponent<TileBehaviour_Water>() != null;
            right = r && r.collider.GetComponent<TileBehaviour_Water>() != null;

            if (left && right)
                waterRenderer.sprite = water_middle;
            else if (!left && right)
                waterRenderer.sprite = water_left;
            else if (left && !right)
                waterRenderer.sprite = water_right;
            else
                waterRenderer.sprite = water_single;
        }

    }

    IEnumerator TileLife()
    {
        float icealpha = 0f;
        while (true)
        {

            switch (Player.actualSeason)
            {
                case Player.Seasons.Winter:
                    icealpha = 1f;
                    gameObject.layer = 8;
                    gameObject.tag = "Untagged";
                    break;
                case Player.Seasons.Spring:
                case Player.Seasons.Summer:
                case Player.Seasons.Autumn:
                    icealpha = 0f;
                    gameObject.layer = 0;
                    gameObject.tag = "death";
                    break;
            }

            ChangeIce(icealpha);

            yield return 0;
        }

    }

    void ChangeIce(float alpha)
    {
        if (iceRenderer != null)
            iceRenderer.color = Color.Lerp(iceRenderer.color, new Color(iceRenderer.color.r, iceRenderer.color.g, iceRenderer.color.b, alpha), 0.05f);
    }

}
