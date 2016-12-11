using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileBehaviour_Tree : MonoBehaviour {

    public ParticleSystem leaf;
    private SerializedObject so;

    private GameObject[] apples;

    public GameObject apple;
    private int lastSeason = 0;
    private bool applePlaced = false;

    void Start()
    {
        so = new SerializedObject(leaf);

        apples = new GameObject[3];

        StartCoroutine(TileLife());
    }

    IEnumerator TileLife()
    {
        while (true)
        {
            yield return new WaitUntil(() => Player.actualSeason >= Player.Seasons.Spring && Player.actualSeason != Player.Seasons.Winter);
            leaf.Play();
            ChangeColor(new Color(187 / 255f, 132 / 255f, 173 / 255f), new Color(146 / 255f, 88 / 255f, 102 / 255f));
            leaf.gravityModifier = 0f;

            yield return new WaitUntil(() => Player.actualSeason >= Player.Seasons.Summer);
            ChangeColor(new Color(9 / 255f, 146 / 255f, 47 / 255f), new Color(121 / 255f, 178 / 255f, 29 / 255f));
            leaf.gravityModifier = 0f;

            if (!applePlaced)
            {
                apple.GetComponent<Rigidbody2D>().gravityScale = 0f;
                apples[0] = Instantiate(apple, new Vector2(transform.position.x - 0.75f, transform.position.y + 2f), new Quaternion(0, 0, 0, 0));
                apples[1] = Instantiate(apple, new Vector2(transform.position.x, transform.position.y + 3f), new Quaternion(0, 0, 0, 0));
                apples[2] = Instantiate(apple, new Vector2(transform.position.x + 0.75f, transform.position.y + 2f), new Quaternion(0, 0, 0, 0));
            }

            yield return new WaitUntil(() => Player.actualSeason >= Player.Seasons.Autumn);
            ChangeColor(new Color(219 / 255f, 66 / 255f, 10 / 255f), new Color(250 / 255f, 204 / 255f, 19 / 255f));
            leaf.gravityModifier = 0f;

            if(!applePlaced)
            {
                foreach (GameObject app in apples)
                {
                    app.GetComponent<Rigidbody2D>().gravityScale = 1f;
                }
                applePlaced = true;
            }

            yield return new WaitForSeconds(1.2f);
            leaf.gravityModifier = 1f;

            yield return new WaitUntil(() => Player.actualSeason != Player.Seasons.Autumn);
            leaf.Stop();
        }

    }

    private void ChangeColor(Color a, Color b)
    {
        if ((so.FindProperty("InitialModule.startColor.minColor") != null) && (so.FindProperty("InitialModule.startColor.maxColor") != null))
        {
            so.FindProperty("InitialModule.startColor.minColor").colorValue = a;
            so.FindProperty("InitialModule.startColor.maxColor").colorValue = b;
            so.ApplyModifiedProperties();
        }
    }

}
