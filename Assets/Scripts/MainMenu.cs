using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnClickEasy()
    {
        CameraMovement.Difficult = 1;
    }
    public void OnClickMedium()
    {
        CameraMovement.Difficult = 2;
    }
    public void OnClickHard()
    {
        CameraMovement.Difficult = 3;
    }
    public void OnClickConfirm()
    {
        SceneManager.LoadScene(1);
    }
    /*
    private void Start()
    {
        SaveLoad.NameOfSave[SaveLoad.i] = "";
    }
    public enum Menu
    {
        MainMenu,
        NewGame,
        Continue
    }
    public Menu currentMenu;
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
       
        if (currentMenu == Menu.MainMenu)
        {
            GUILayout.Box("FreeLands");
            GUILayout.Space(10);
            if (GUILayout.Button("New Game"))
            {
                SaveLoad.current = new SaveLoad();
                currentMenu = Menu.NewGame;
            }
            if (GUILayout.Button("Continue"))
            {
                currentMenu = Menu.Continue;
                SaveLoad.Load();
            }
            if (GUILayout.Button("Quit"))
            {
                Application.Quit();
            }
        }
        else if (currentMenu == Menu.NewGame)
        {
            
            GUILayout.Box("Choose Your Difficult");
            GUILayout.Space(10);
            var dateTime = DateTime.Now.Date;
            var dateDay = DateTime.Now.Day;
            var dateMonth = DateTime.Now.Month;
            var dateYear = DateTime.Now.Year;
            if (GUILayout.Button("Easy"))
            {
                GUI.color = Color.green;
                GUI.backgroundColor = Color.red;
                //currentStyle_1.normal.background = MakeTex(2, 2, new Color(0f, 1f, 0f, 0.5f));
                SaveLoad.current.Difficult = 1;

                SaveLoad.NameOfSave[SaveLoad.i] = "Easy." + dateDay + "." + dateMonth + "." + dateYear;
                Debug.Log(SaveLoad.NameOfSave[SaveLoad.i]);
            }
            if (GUILayout.Button("Medium"))
            {
                SaveLoad.current.Difficult = 2;
                SaveLoad.NameOfSave[SaveLoad.i] = "Medium." + dateDay + "." + dateMonth + "." + dateYear;
                Debug.Log(SaveLoad.NameOfSave[SaveLoad.i]);
            }
            if (GUILayout.Button("Hard"))
            {
                SaveLoad.current.Difficult = 3;
                SaveLoad.NameOfSave[SaveLoad.i] = "Hard." + dateDay + "." + dateMonth + "." + dateYear;
                Debug.Log(SaveLoad.NameOfSave[SaveLoad.i]);
            }
            GUILayout.Box("Name of the save");
            SaveLoad.NameOfSave[SaveLoad.i] = GUILayout.TextField(SaveLoad.NameOfSave[SaveLoad.i], 20);
            if (GUILayout.Button("Save"))
            {
                SaveLoad.i++;
                Debug.Log(SaveLoad.i);
                Debug.Log(SaveLoad.NameOfSave[SaveLoad.i]);
                SceneManager.LoadScene(1);
                SaveLoad.Save();
            }
            GUILayout.Space(10);
            if (GUILayout.Button("Cancel"))
            {
                currentMenu = Menu.MainMenu;
            }
        }
        else if (currentMenu == Menu.Continue)
        {
            GUILayout.Box("Select Save File");
            GUILayout.Space(10);
            foreach (SaveLoad d in SaveLoad.savedGames)
            {
                if (GUILayout.Button(SaveLoad.NameOfSave[SaveLoad.i]))
                {
                    SaveLoad.current = d;
                    SceneManager.LoadScene(1);
                }
            }

            if (GUILayout.Button("Cancel"))
            {
                currentMenu = Menu.MainMenu;
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }*/
}

