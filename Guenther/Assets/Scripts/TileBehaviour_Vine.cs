using System.Collections;
using UnityEngine;

public class TileBehaviour_Vine : MonoBehaviour{
    public Sprite top, mid;
    private SpriteRenderer render;

    void Start() {
        render = GetComponent<SpriteRenderer>();
        if (render == null)
        {
            render = gameObject.AddComponent<SpriteRenderer>();
        }
        render.sprite = top;
        StartCoroutine(SeasonUpdate());
    }

    IEnumerator SeasonUpdate() {
        System.Func<bool> isSpring = ()=> Player.actualSeason == Player.Seasons.Spring;
        System.Func<bool> isSummer = () => Player.actualSeason == Player.Seasons.Summer;
        RaycastHit2D hit;
        while (true)
        {
            // do stuff in spring
            {
                // check if there is anything above me
                hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
                if (hit || !isSpring()) { }
                else
                {
                    // if free grow the vine out
                    yield return new WaitForSeconds(0.1f);
                    GameObject obj = Instantiate(gameObject);
                    obj.transform.SetParent(transform.parent);
                    obj.transform.position = transform.position + Vector3.up;
                    render.sprite = mid;
                }
            }
            // undo stuff after spring
            yield return new WaitUntil(isSummer);
            // if vine below
            hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
            if (hit && hit.collider.GetComponent<TileBehaviour_Vine>() != null)
            {
                name = "DummyVine";
                yield return new WaitUntil(isMyTurnToDie);
                render.sprite = top;
                break;
            }
            else
            {
                name = "root";
                yield return new WaitUntil(isTopVine);
                render.sprite = top;
            }
            // back to spring
            yield return new WaitUntil(isSpring);
        }
        Destroy(gameObject, 0.1f);
    }

    private bool isTopVine()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if (hit && hit.collider.GetComponent<TileBehaviour_Vine>() != null)
        {
            // not top vine collides with a vine on top
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool isMyTurnToDie()
    {
        if (Player.actualSeason == Player.Seasons.Spring)
        {
            return true;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if (hit) //if something above
        {
            if (hit.collider.GetComponent<TileBehaviour_Vine>() != null)
            {
                //if its a vine dont die yet
                return false;
            }
        }
        return true;
    }

    void OnDeath()
    {
        StopAllCoroutines(); 
    }
}
