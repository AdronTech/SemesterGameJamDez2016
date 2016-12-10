using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Base : MonoBehaviour, ITile {

    public Sprite solid, bottom;
    private SpriteRenderer sr;

    void ITile.Init()
    {

        GameObject go = new GameObject("Base");
        sr = go.AddComponent<SpriteRenderer>();
        go.transform.SetParent(transform);
        go.transform.position = transform.position;
        sr.sortingOrder = transform.childCount;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if(hit && hit.collider.GetComponent<TileBehaviour_Base>() != null)
        {
            sr.sprite = solid;
            return;
        }

        sr.sprite = bottom;
    }
}
