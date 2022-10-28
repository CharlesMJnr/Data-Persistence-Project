using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenuManager : MonoBehaviour
{
    public string playerName;
    public static string bestPlayerName;
    public static int bestScore = 0;
    public TextMeshProUGUI bestScoreText;

    public static StartMenuManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(bestScore == 0 && playerName != "") 
        {
            bestPlayerName = playerName;
        }
        if(bestPlayerName != null) 
        {
            bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}";
        }

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }
    
    public void UpdatePlayerName(string name)
    {
        playerName = name;
    }

    [System.Serializable]
    class SaveData 
    {
        public string BestPlayerName;
        public int BestScore;
    }

    public void SaveBestScore() 
    {
        SaveData data = new SaveData();
        data.BestPlayerName = bestPlayerName;
        data.BestScore = bestScore;

        string Json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", Json);
    }

    public void LoadBestScore() 
    {
        string path = Application.persistentDataPath + "/savefile.json",Json;
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.BestPlayerName;
            bestScore = data.BestScore;
        }

        }
}
