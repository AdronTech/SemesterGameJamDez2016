using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Water : MonoBehaviour, ITile {

    public Sprite water, water_single, water_left, water_middle, water_right, ice;
    private SpriteRenderer waterRenderer, iceRenderer;

    void ITile.Init()
    {

        GameObject waterGO = new GameObject("Water");
        waterRenderer = waterGO.AddComponent<SpriteRenderer>();
        waterGO.transform.SetParent(transform);
        waterGO.transform.position = transform.position;
        waterRenderer.sortingOrder = transform.childCount;

        waterRenderer.sprite = water;

        GameObject iceGO = new GameObject("Ice");
        iceRenderer = iceGO.AddComponent<SpriteRenderer>();
        iceGO.transform.SetParent(transform);
        iceGO.transform.position = transform.position;
        iceRenderer.sortingOrder = transform.childCount;


        // ice
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);

        if (!(hit && hit.collider.GetComponent<TileBehaviour_Base>() != null))
        {
            iceRenderer.sprite = ice;
        }

        // water
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if(hit && hit.collider.GetComponent<TileBehaviour_Base>() != null)
        {
            bool left, right;
            RaycastHit2D l = Physics2D.Raycast(transform.position, Vector2.left, 1f);
            RaycastHit2D r = Physics2D.Raycast(transform.position, Vector2.left, 1f);
            
            left = l && l.collider.GetComponent<TileBehaviour_Water>() != null;
            right = r && r.collider.GetComponent<TileBehaviour_Water>() != null;

            if (left && right)
                waterRenderer.sprite = water_middle;
            else if(!left && right)
                waterRenderer.sprite = water_left;
            else if (left && !right)
                waterRenderer.sprite = water_right;
            else 
                waterRenderer.sprite = water_single;
        }

    }

}
