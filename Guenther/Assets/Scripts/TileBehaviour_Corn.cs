﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Corn : MonoBehaviour
{

    public Sprite top, middle, bottom;
    public float BuildSpeed;
    private SpriteRenderer topRenderer, middleRenderer, bottmoRenderer;
    private Transform topTransform, middleTransform;

    void Awake()
    {

        // Bottom
        GameObject bottomGo = new GameObject("Bottom");
        bottmoRenderer = bottomGo.AddComponent<SpriteRenderer>();
        bottomGo.transform.SetParent(transform);
        bottomGo.transform.position = transform.position;

        bottmoRenderer.sortingOrder = transform.childCount;
        bottmoRenderer.sprite = bottom;

        // Middle
        GameObject middleGo = new GameObject("Middle");
        middleRenderer = middleGo.AddComponent<SpriteRenderer>();
        middleGo.AddComponent<BoxCollider2D>();
        middleGo.layer = 8;
        middleGo.transform.SetParent(transform);
        middleGo.transform.position = transform.position;
        middleTransform = middleGo.transform;

        middleRenderer.sortingOrder = transform.childCount;
        middleRenderer.sprite = middle;

        // Top
        GameObject topGo = new GameObject("Top");
        topRenderer = topGo.AddComponent<SpriteRenderer>();
        topGo.transform.SetParent(transform);
        topGo.transform.position = transform.position;
        topTransform = topGo.transform;

        topRenderer.sortingOrder = transform.childCount;
        topRenderer.sprite = top;
    }

    void Start()
    {
        StartCoroutine(TileLife());
    }

    IEnumerator TileLife()
    {
        float height = 0;
        Color c = Color.green;
        float alpha = 0;
        while (true)
        {
            switch (Player.actualSeason)
            {
                case Player.Seasons.Winter:
                    height = 1;
                    c = new Color(150 / 255f, 149 / 255f, 44 / 255f);
                    alpha = 1;
                    break;
                case Player.Seasons.Spring:
                    height = 2;
                    c = new Color(89 / 255f, 133 / 255f, 47 / 255f);
                    alpha = 1;
                    break;
                case Player.Seasons.Summer:
                    height = 3;
                    c = new Color(248 / 255f, 162 / 255f, 10 / 255f);
                    alpha = 1;
                    break;
                case Player.Seasons.Autumn:
                    height = 0;
                    c = new Color(145 / 255f, 120 / 255f, 90 / 255f);
                    alpha = 0;
                    break;
            }

            ChangeHeight(height);
            ChangeColor(c);
            ChangeAlpha(alpha);

            yield return 0;
        }

    }

    void ChangeHeight(float h)
    {

        middleTransform.localScale = Vector3.Lerp(middleTransform.localScale, new Vector3(1, h, 1), BuildSpeed);
        float middleY = -0.5f + middleTransform.localScale.y * 0.5f;
        middleTransform.localPosition = new Vector3(middleTransform.localPosition.x, middleY);

        float topY = middleTransform.position.y + middleTransform.localScale.y / 2 + 0.5f;
        topTransform.position = new Vector3(topTransform.position.x, topY);
    }

    void ChangeColor(Color c)
    {
        topRenderer.color = Color.Lerp(topRenderer.color, c, BuildSpeed);
        middleRenderer.color = Color.Lerp(middleRenderer.color, c, BuildSpeed);
        bottmoRenderer.color = Color.Lerp(bottmoRenderer.color, c, BuildSpeed);
    }

    void ChangeAlpha(float alpha)
    {
        topRenderer.color = Color.Lerp(topRenderer.color, new Color(topRenderer.color.r, topRenderer.color.g, topRenderer.color.b, alpha), 2 * BuildSpeed);
    }

}
