using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Gesture> Gestures = new List<Gesture>();

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.streamingAssetsPath+"/XML/spells/", "*.xml");
        foreach (string i in gestureFiles)
        {
            Gestures.Add(GestureIO.ReadGestureFromFile(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
