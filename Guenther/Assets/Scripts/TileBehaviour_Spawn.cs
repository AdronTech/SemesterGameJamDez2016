using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour_Spawn : MonoBehaviour {

    public GameObject playerPrefab;
    public AudioClip voiceIntro, musicLoop;
    void Start()
    {
        Player player = Instantiate(playerPrefab).GetComponent<Player>();
        if (player)
        {
            //spawn player
            player.transform.position = transform.position;
            //respawn watcher
            StartCoroutine(AudioStart());
            StartCoroutine(PlayerRespawn(player));
        }
        else
        {
            Debug.Log("SpawnTile: Can not instantiate Player Prefab. Oh Shit.");
        }


    }

    IEnumerator AudioStart()
    {
        AudioSource src = GetComponent<AudioSource>();
        src.loop = false;
        src.clip = voiceIntro;
        src.Play();
        yield return new WaitUntil(() => !src.isPlaying);
        src.loop = true;
        src.clip = musicLoop;
        src.Play();
    }

    IEnumerator PlayerRespawn(Player player)
    {
        System.Func<bool> isDeth = () => Player.death;
        while (true)
        {
            yield return new WaitUntil(isDeth);
            player.transform.position = transform.position;
            //respawn
        }
    }
}
