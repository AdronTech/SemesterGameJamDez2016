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

    private Text[] Buttons = new Text[3];

    public Texture texture0;
    public Texture texture1;
    public Texture texture2;

    private Texture[] textures = new Texture[3];
    
    //private static Text button0text; 
    //private static Text button1text;
    //private static Text button2text;

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
        for (i = 0; i < (length); i = i + 2) {
            levelArray[j] = (tmp[i].Split('.'))[0];
            Debug.Log(levelArray[j]); //alle namen deer Szenen enthalten
            j++;
        }

       
    }

   private void OnGUI() {  


        for(int i = 0; i < levelArray.Length; i++){

            //GUI.Label(new Rect(0 + i*10, 0, 100, 100), "I'm a GUI");

            //gebe namen dem button den indix an im array -> lade szene im array am index mit dem namen vom wert im array drin
            if (GUI.Button(new Rect(200 + (-100 + (600 *i)), 280 , 500, 290), textures[i])) {
                isDown = i;


                switch (i)
                {
                    case 0: Buttons[0].enabled = true; Buttons[1].enabled = false; Buttons[2].enabled = false;  break;
                    case 1: Buttons[1].enabled = true; Buttons[0].enabled = false; Buttons[2].enabled = false; break;
                    case 2: Buttons[2].enabled = true; Buttons[0].enabled = false; Buttons[1].enabled = false; break;
                    default: break;
                }              
            } 
        }        
    }

    public void LoadByIndex()
    {
        if(isDown!=-1)
        SceneManager.LoadScene(levelArray[isDown]);
    }

    // Use this for initialization
    void Start() {

        textures[0] = texture0;
        textures[1] = texture1;
        textures[2] = texture2;

        Buttons[0] = this.transform.GetChild(2).GetComponent<Text>();
        Buttons[1] = this.transform.GetChild(3).GetComponent<Text>();
        Buttons[2] = this.transform.GetChild(4).GetComponent<Text>();

        Buttons[0].enabled = false;
        Buttons[1].enabled = false;
        Buttons[2].enabled = false;

        OnGUI();     
        
           
    }

    // Update is called once per frame
    void Update() {

    }
}
