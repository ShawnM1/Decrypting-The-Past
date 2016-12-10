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
     // if we are playing from editor, create fake save file
#if DEBUG
        //CreateNewSaveFile("test.dtp");
#endif
    }
    /// <summary>
    /// Loads save file
    /// </summary>
    /// <param name="fileName"></param>
    public void LoadSaveFile(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            originalPath = Application.persistentDataPath + "/" + fileName;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = File.Open(originalPath, FileMode.Open);
            SaveFile = (SaveFile)bf.Deserialize(stream);
            stream.Close();
            GoToScene.goToScene("TimeLineMenuScene");
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
        }
    }
    /// <summary>
    /// Static singleton instance of the save container
    /// </summary>
    public static SaveContainer Instance
    {
        get
        {
            if (instance == null)
            {
                return new SaveContainer();
            }
            else
            {
                return instance;
            }
        }
    }
    private void Awake()
    {
        // if the singleton hasn't been initialized yet. 
        // If thre is an instance and instance isn't this, then destroy
        // the other instance and make this the instance
        //if (instance != ull && instance != this)
        print("Instance: " + instance + "Are we instantce?: " + (instance != this));
        if (instance != null && instance != this)
        {
            print("Destroy Called");
            Destroy(this.gameObject);
        }
        print("Singleton Awwake Called");
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
