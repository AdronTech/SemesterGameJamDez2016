using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileBehaviour_Victory : MonoBehaviour {

	void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(4); 
        }
    }
}
