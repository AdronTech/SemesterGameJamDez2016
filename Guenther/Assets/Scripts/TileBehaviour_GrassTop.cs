using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_GrassTop : MonoBehaviour, ITile {

    public Sprite grass;
    private SpriteRenderer renderer;

    public void Init()
    {
        //ground beneath
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
        if (hit && hit.collider.GetComponent<TileBehaviour_Base>() != null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = grass;
        }
    }

    void Start()
    {

    }
	
}
