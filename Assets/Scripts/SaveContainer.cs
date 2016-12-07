using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveContainer : MonoBehaviour {

    private static SaveContainer instance = null;
    public SaveFile SaveFile;
    string originalPath = "";
    void Start()
    {
#if DEBUG
        CreateNewSaveFile("test.dtp");
#endif
    }
    public void LoadSaveFile(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            originalPath = Application.persistentDataPath + "/" + fileName;
            print("Loading From: " + originalPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = File.Open(originalPath, FileMode.Open);
            SaveFile = (SaveFile)bf.Deserialize(stream);
            stream.Close();
            StartCoroutine(GoToScene.GoToSceneEnumerator("TimeLineMenuScene"));
        }
    }
    public void CreateNewSaveFile(string fileName)
    {
        originalPath = Application.persistentDataPath + "/" + fileName;
        SaveFile = new SaveFile();
        SaveFile.LastLevelPlayed = "CaesarCipher";
        SaveDataToFile();
    }
    public void SaveDataToFile()
    {
        if(originalPath != null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = File.Create(originalPath);
            bf.Serialize(stream, SaveFile);
            stream.Close();
            print("Saving to: " + originalPath);
        }
    }
    public static SaveContainer Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public static bool DoSaveFilesExist()
    {
        print(Application.persistentDataPath);
        if(Directory.GetFiles(Application.persistentDataPath,"*.dtp").Length > 0)
        {
            return true;
        }
        return false;
    }
    
}
