using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Lava : MonoBehaviour, ITile {

    public Sprite lava, lava_single, lava_left, lava_middle, lava_right;
    private SpriteRenderer sr;

    void ITile.Init()
    {

        GameObject go = new GameObject("Lava");
        sr = go.AddComponent<SpriteRenderer>();
        go.transform.SetParent(transform);
        go.transform.position = transform.position;

        sr.sortingOrder = transform.childCount;
        sr.sprite = lava;
        sr.color = Color.red; // new Color(255 / 255f, 202 / 255f, 25 / 255f);

        // water
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (!(hit && hit.collider.GetComponent<TileBehaviour_Lava>() != null))
        {
            bool left, right;
            RaycastHit2D l = Physics2D.Raycast(transform.position, Vector2.left, 1f);
            RaycastHit2D r = Physics2D.Raycast(transform.position, Vector2.right, 1f);

            left = l && l.collider.GetComponent<TileBehaviour_Lava>() != null;
            right = r && r.collider.GetComponent<TileBehaviour_Lava>() != null;

            if (left && right)
                sr.sprite = lava_middle;
            else if (!left && right)
                sr.sprite = lava_left;
            else if (left && !right)
                sr.sprite = lava_right;
            else
                sr.sprite = lava_single;
        }

    }

}
