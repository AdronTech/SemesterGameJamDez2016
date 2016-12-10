using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

    //schaue wie viele levels im levels 
    //in Awake
    //array erstellen
    //array aus szenen
    //so viele buttons erstellen
    
    private static Text button0text;
    private static Text button1text;
    private static Text button2text;

    private static int isDown = -1;

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

   private void OnGUI() {  


        for(int i = 0; i < levelArray.Length; i++){

            //GUI.Label(new Rect(0 + i*10, 0, 100, 100), "I'm a GUI");

            //gebe namen dem button den indix an im array -> lade szene im array am index mit dem namen vom wert im array drin
            if (GUI.Button(new Rect(165 + 12 + (-165 + (165 *i)), 100 , 160, 90), "Click")) {
                isDown = i;
                switch (i)
                {
                    case 0: button0text.enabled = true; button1text.enabled = false; button2text.enabled = false;  break;
                    case 1: button1text.enabled = true; button0text.enabled = false; button2text.enabled = false; break;
                    case 2: button2text.enabled = true; button0text.enabled = false; button1text.enabled = false; break;
                    default: break;
                }              
            } 
        }        
    }

    public void LoadByIndex(int buttonnr)
    {
        SceneManager.LoadScene(levelArray[buttonnr]);
    }

    // Use this for initialization
    void Start() {

        button0text = this.transform.GetChild(2).GetComponent<Text>();
        button1text = this.transform.GetChild(3).GetComponent<Text>();
        button2text = this.transform.GetChild(4).GetComponent<Text>();

        button0text.enabled = false;
        button1text.enabled = false;       
        button2text.enabled = false;

        OnGUI();        
    }

    // Update is called once per frame
    void Update() {

    }
}
