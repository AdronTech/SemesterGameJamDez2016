using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager {

    private static AppleManager instance;
    private int applenr = 0;

    private AppleManager() {
        //sicher gehen, dass nur eine instanz erschaffen
        //aber redundand, weil private -> nur in script änderbar und hier * macht
        if (instance != null)
            return;
        instance = this;        
    }

    public static AppleManager Instance { //schreibgeschützt
        get { //da drauf zugreifen
              //*hier
            if (instance == null)
                instance = new AppleManager();//neue Instanz erstellt
            return instance;
        }
    }

    public int Applenr {
        get { return applenr; } //oder auch if(score <10) gebe nur dann zurück
        set { applenr = value; } //value ist schon intern initialisiert, müssen nicht extra mit Übergabeparameter
                               //statt setScore(10);
    }
}
