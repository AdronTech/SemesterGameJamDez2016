using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Spawn : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start() {
        //player.transform.position = transform.position;
        StartCoroutine(PlayerSpawn());
    }

    // Update is called once per frame
    IEnumerator PlayerSpawn() {
        System.Func<bool> onDeath = () => Player.death;
        yield return new WaitUntil(onDeath);
        player.transform.position = transform.position;
        //player died
        yield return new WaitWhile(onDeath);
    }
}
