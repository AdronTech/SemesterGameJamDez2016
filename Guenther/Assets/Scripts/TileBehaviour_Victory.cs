using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileBehaviour_Victory : MonoBehaviour {

    public AudioClip victorySound;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory()
    {
        AudioSource src = GetComponent<AudioSource>();
        if (src == null)
        {
            src = gameObject.AddComponent<AudioSource>();
        }
        src.clip = victorySound;
        src.loop = false;
        src.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
    }
}
