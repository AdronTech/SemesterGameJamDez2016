using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Tree : MonoBehaviour {

    public ParticleSystem leaf;
    //private UnityEditor.SerializedObject so;

    private GameObject[] apples;

    public GameObject apple;
    private int lastSeason = 0;
    private bool applePlaced = false;

    void Start()
    {
        //so = new UnityEditor.SerializedObject(leaf);

        apples = new GameObject[3];

        StartCoroutine(TileLife());
    }

    IEnumerator TileLife()
    {
        while (true)
        {
            yield return new WaitUntil(() => Player.actualSeason >= Player.Seasons.Spring && Player.actualSeason != Player.Seasons.Winter);
            leaf.Play();
            ChangeColor(new Color(150 / 255f, 255 / 255f, 150 / 255f));
            leaf.gravityModifier = 0f;

            yield return new WaitUntil(() => Player.actualSeason >= Player.Seasons.Summer);
            ChangeColor(new Color(9 / 255f, 146 / 255f, 47 / 255f));
            leaf.gravityModifier = 0f;

            if (!applePlaced)
            {
                apple.GetComponent<Rigidbody2D>().gravityScale = 0f;
                apple.GetComponent<BoxCollider2D>().enabled = false;
                apples[0] = Instantiate(apple, new Vector2(transform.position.x - 0.75f, transform.position.y + 2f), new Quaternion(0, 0, 0, 0));
                apples[1] = Instantiate(apple, new Vector2(transform.position.x, transform.position.y + 3f), new Quaternion(0, 0, 0, 0));
                apples[2] = Instantiate(apple, new Vector2(transform.position.x + 0.75f, transform.position.y + 2f), new Quaternion(0, 0, 0, 0));
            }

            yield return new WaitUntil(() => Player.actualSeason >= Player.Seasons.Autumn);
            ChangeColor(new Color(200 / 255f, 100 / 255f, 10 / 255f));
            leaf.gravityModifier = 0f;

            if(!applePlaced)
            {
                foreach (GameObject app in apples)
                {
                    app.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    app.GetComponent<BoxCollider2D>().enabled = true;
                }
                applePlaced = true;
            }

            yield return new WaitForSeconds(1.2f);
            leaf.gravityModifier = 1f;

            yield return new WaitUntil(() => Player.actualSeason != Player.Seasons.Autumn);
            leaf.Stop();
        }

    }

    private void ChangeColor(Color a)
    {
        // insert gradient;
        ParticleSystem.MinMaxGradient gradient = new ParticleSystem.MinMaxGradient(a);
        ParticleSystem.ColorOverLifetimeModule clt = leaf.colorOverLifetime;
        clt.color = gradient;
        clt.enabled = true;
        //if ((so.FindProperty("InitialModule.startColor.minColor") != null) && (so.FindProperty("InitialModule.startColor.maxColor") != null))
        //{
        //    so.FindProperty("InitialModule.startColor.minColor").colorValue = a;
        //    so.FindProperty("InitialModule.startColor.maxColor").colorValue = b;
        //    so.ApplyModifiedProperties();
        //}
    }

}
