using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Spawn : MonoBehaviour {

    public GameObject playerPrefab;

    void Start()
    {
        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        if (player)
        {
            //spawn player
            player.transform.position = transform.position;
            //respawn watcher
            StartCoroutine(PlayerRespawn(player));
        }
        else
        {
            Debug.Log("SpawnTile: Can not instantiate Player Prefab. Oh Shit.");
        }


    }

    IEnumerator PlayerRespawn(Player player)
    {
        System.Func<bool> isDeth = () => Player.death;
        while (true)
        {

            yield return new WaitUntil(isDeth);
            yield return new WaitForSeconds(7);
            player.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            //respawn
            player.GetComponent<Animator>().SetBool("isDead", false);
           Player.death = false;
        }
    }
}
