using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainMenuButtons : MonoBehaviour {

    //schaue wie viele levels im levels 
    //in Awake
    //array erstellen
    //array aus szenen
    //so viele buttons erstellen

    string[] levelArray;

    void Awake() {
        string szenenpfad = "Assets//Scenes//Levels";
        DirectoryInfo di = new DirectoryInfo(szenenpfad);
        int length = di.GetFiles().Length;
        Debug.Log("Anzahl der Scenes im Levels: " + length);

        levelArray = new string[length];
        //namen der dateien        

        int i = 0;
        foreach (string file in System.IO.Directory.GetFiles(szenenpfad)) {

            Debug.Log(file);

            string[] nameFile = file.Split('\\');

            levelArray[i] = nameFile[nameFile.Length - 1];

            Debug.Log(levelArray[i]); //array voller cyprian.unity.meta

            i++;
        }

        string[] tmp = levelArray;
        levelArray = new string[length / 2];

        int j = 0;
        //jedes zweite ist .meta -> brauch ich nicht
        for (i = 0; i < length / 2; i = i + 2) {
            levelArray[j] = (tmp[i].Split('.'))[0];
            Debug.Log(levelArray[j]); //alle namen deer Szenen enthalten
            j++;
        }

       
    }

    void OnGUI() {
        


        for(int i = 0; i < levelArray.Length; i++){

            GUI.Label(new Rect(0 + i*10, 0, 100, 100), "I'm a GUI");

            //gebe namen dem button den indix an im array -> lade szene im array am index mit dem namen vom wert im array drin
            if (GUI.Button(new Rect(10, 10, 150, 100), "Click")) {
            Debug.Log("Button Clicked");
        }

        }
    }

    // Use this for initialization
    void Start() {
        OnGUI();
    }

    // Update is called once per frame
    void Update() {

    }
}
