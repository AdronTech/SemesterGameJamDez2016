using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Wasser : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(TileLife());
	}
	
    IEnumerator TileLife()
    {
        System.Func<bool> isWinter = () => Player.actualSeason == Player.Seasons.Winter;
        yield return new WaitUntil(isWinter);
        // change to winter
        yield return new WaitWhile(isWinter);
    }

}