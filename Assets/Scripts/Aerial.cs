using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerial : MonoBehaviour
{
    private GameObject Player;
    private GameObject NextLevel;
    private void Start()
    {
        NextLevel = GetComponentInChildren<GameObject>();
    }
    private void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            Player = Col.gameObject;
            Player.tag = "Charging";
            NextLevel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider Col)
    {
        Player = Col.gameObject;
        Player.tag = "Player";
        NextLevel.SetActive(false);
    }
}