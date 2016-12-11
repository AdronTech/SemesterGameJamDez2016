﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Tree : MonoBehaviour {

    public ParticleSystem leaf;
    
    public GameObject apple;
    private int lastSeason = 0;
    private bool applePlaced = false; 
	
	// Update is called once per frame
	void Update () {
		if(lastSeason != (int)Player.actualSeason)
        {
            if(Player.actualSeason == Player.Seasons.Autumn && !applePlaced)
            {
                Instantiate(apple, new Vector2(transform.position.x - 1, transform.position.y), new Quaternion(0, 0, 0, 0));
                Instantiate(apple, new Vector2(transform.position.x, transform.position.y + 1), new Quaternion(0, 0, 0, 0));
                Instantiate(apple, new Vector2(transform.position.x + 1, transform.position.y), new Quaternion(0, 0, 0, 0));
                applePlaced = true;
            }
        }
	}
}
