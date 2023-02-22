using PDollarGestureRecognizer;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectPoolHandler ObjectPoolHandler;
    public Character Player;
    public List<Gesture> Gestures = new List<Gesture>();

    public List<SOspell> Spells = new List<SOspell>();

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.streamingAssetsPath + "/XML/spells/", "*.xml");
        foreach (string i in gestureFiles)
        {
            Gestures.Add(GestureIO.ReadGestureFromFile(i));
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
