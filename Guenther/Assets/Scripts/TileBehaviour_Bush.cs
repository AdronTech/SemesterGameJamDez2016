using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Bush : MonoBehaviour {

    public ParticleSystem leaf, fire;

    void Start()
    {
        fire.Stop();
        leaf.Stop();
        StartCoroutine(TileLife());
        gameObject.tag = "death";
    }

    IEnumerator TileLife()
    {
        while(true)
        {
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

            switch (Player.actualSeason)
            {
                case Player.Seasons.Spring:
                    leaf.Play();
                    break;
                case Player.Seasons.Summer:
                    transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    leaf.Stop();
                    fire.Play();
                    break;
                case Player.Seasons.Autumn:
                    fire.Stop();
                    break;
                case Player.Seasons.Winter:
                    break;
            }

            yield return 0;

        }
    }
}