using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Vine : MonoBehaviour {

    public Sprite top, mid;
    private SpriteRenderer renderer;
    void Start() {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = top;
        StartCoroutine(SeasonUpdate());
    }
    
    IEnumerator SeasonUpdate() {
        System.Func<bool> isSpring = ()=> Player.actualSeason == Player.Seasons.Winter;
        while (true)
        {
            // do stuff in spring
            yield return new WaitUntil(isSpring);
            if (SpaceSet(Vector3.up, null))
            {
                yield return new WaitForSeconds(0.1f);
                GameObject obj = Instantiate(gameObject);
                obj.transform.SetParent(transform.parent);
                obj.transform.position = transform.position + Vector3.up;
                renderer.sprite = mid;
            }

            // undo stuff after spring
            yield return new WaitWhile(isSpring);
            while (!isSpring())
            {
                if (SpaceSet(Vector3.up, "vines")) //if vine above wait
                {
                    yield return new WaitForSeconds(0.1f);
                }
                else //delete after 0.5 if vine below
                {
                    renderer.sprite = top;
                    if (SpaceSet(Vector3.down, "vines"))
                    {
                        Destroy(gameObject, 0.5f);
                    }
                    break;
                }
            }
        }
    }

    private bool SpaceSet(Vector3 dir, string t)
    {
        // return if there is something above me
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1);
        if (hit)
        {
            // if parameter null collide with anything
            if(tag == null)
            {
                return true;
            }
            else
            // else hit only things with similar tag
            {
                return hit.collider.tag == t;
            }
        }
        else
        {
            return false;
        }
    }
}
