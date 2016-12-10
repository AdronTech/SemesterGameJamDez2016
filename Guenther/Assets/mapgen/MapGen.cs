using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapGen : ScriptableObject {
    public Texture2D bitmap;
    public bool onValidateRebuild = false;

    public void Extract()
    {
        Dictionary<Color, GameObject> objectmap = new Dictionary<Color, GameObject>();
        foreach(Entry e in dictionary)
        {
            if (e.value != null) {
                objectmap.Add(e.key, e.value);
            }
        }
        GameObject obj = new GameObject("World");
        Transform parent = obj.transform;
        for(int x = 0; x < bitmap.width; x++)
        {
            for(int y = 0; y < bitmap.height; y++)
            {
                Color c = bitmap.GetPixel(x, y);
                if(objectmap.TryGetValue(c, out obj))
                {
                    obj = Instantiate(obj);
                    obj.transform.position = new Vector3(x, y, 0);
                    obj.transform.SetParent(parent);
                }
            }
        }

        foreach (ITile tile in parent.GetComponentsInChildren<ITile>())
        {
            tile.Init();
        } 
    }

    #region Inspector is stupid
    [System.Serializable]
    public struct Entry
    {
        public string name;
        public Color key;
        public GameObject value;
    }
    public Entry[] dictionary;
    #endregion

    void OnValidate()
    {
        if (onValidateRebuild)
        {
            Extract();
        }
    }
}
