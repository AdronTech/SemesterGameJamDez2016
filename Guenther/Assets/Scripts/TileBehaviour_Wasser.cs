using System.Collections;
using UnityEngine;


public class TileBehaviour_Wasser : MonoBehaviour {

    public Sprite eisSprite;
    public Sprite wasserSprite;

	// Use this for initialization
	void Start () {
        StartCoroutine(TileLife());
	}
	
    IEnumerator TileLife()
    {
        System.Func<bool> isWinter = () => Player.actualSeason == Player.Seasons.Winter;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        while (true)
        {
            yield return new WaitWhile(isWinter);
            tag = "water";
            

            yield return new WaitUntil(isWinter);
            tag = "frozen";

        }

    }

}
