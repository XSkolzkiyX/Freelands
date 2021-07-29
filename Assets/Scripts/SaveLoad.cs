using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[System.Serializable]
public class Data
{
    public int Difficult;
    public float[] positionX, positionY;
    public Data(SceneRandomizer S)
    {
        for (int i = 0; i < S.RockRange;i++)
        {
            positionX[i] = S.RockX[i];
        }
        positionY = S.RockY;
    }
}
public class SaveLoad : MonoBehaviour
{
    public int Difficult = 2;
    public static int i = 0;
    public static SaveLoad current;
    public static string[] NameOfSave = new string[10];
    public static List<SaveLoad> savedGames = new List<SaveLoad>();
    public static void SavePlayer(SceneRandomizer scene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        Data D = new Data(scene);
        formatter.Serialize(stream, D);
        stream.Close();
    }
    public static Data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/save.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static void Save()
    {
        savedGames.Add(current);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        //FileStream file = File.Create(Application.persistentDataPath + NameOfSave); //you can call it anything you want
        string path = Application.persistentDataPath + "/save.txt";
        FileStream file = new FileStream(path, FileMode.Create);
        string TEST = "Just a test";
        bf.Serialize(file, TEST);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + NameOfSave))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + NameOfSave, FileMode.Open);
            savedGames = (List<SaveLoad>)bf.Deserialize(file);
            file.Close();
        }
    }
}
