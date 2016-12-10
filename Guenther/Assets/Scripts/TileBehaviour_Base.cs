using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Base : MonoBehaviour {

    private static Sprite solid, bottom;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        if (solid == null)
            solid = Resources.Load<Sprite>("Textures/dirt_solid");
        if (bottom == null)
            bottom = Resources.Load<Sprite>("Textures/dirt_bottom");

        GameObject go = new GameObject("Base");
        go.AddComponent<SpriteRenderer>();
        go.transform.SetParent(transform);
        go.transform.position = transform.position;

        sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = solid;

        Check();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Check()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if(hit)
        {
            if(hit.collider.GetComponent<TileBehaviour_Base>() != null)
            {
                sr.sprite = solid;
                return;
            }
        }

        sr.sprite = bottom;
    }
}
