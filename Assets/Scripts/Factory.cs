using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public GameObject Drone1Prefab, Drone2Prefab, Drone3Prefab, Drone4Prefab, Drone5Prefab;
    public Button Droid1, Droid2, Droid3, Droid4, Droid5;
    public Text IronText, CoalText, RockText, LiquidText, IT, CT, RT, LT, LoadingText;
    public GameObject OpenButton, CloseButton, Panel;
    public float Iron = 0, Coal = 0, Rock = 0, Liquid = 0;
    public bool NearFactory = false, DropRes = false, CoolDownBool;
    void FixedUpdate()
    {
        IT.text = "Iron: " + Iron;
        CT.text = "Coal: " + Coal;
        RT.text = "Rock: " + Rock;
        LT.text = "Liquid: " + Liquid;
        IronText.text = "Iron: " + Iron;
        CoalText.text = "Coal: " + Coal;
        RockText.text = "Rock: " + Rock;
        LiquidText.text = "Liquid: " + Liquid;
        #region IF
        if (Iron >= 40 && Coal >= 30 && Liquid >= 20)
        {
            Droid1.interactable = true;
        }
        else
        {
            Droid1.interactable = false;
        }
        if (Iron >= 50 && Coal >= 30 && Liquid >= 10)
        {
            Droid2.interactable = true;
        }
        else
        {
            Droid2.interactable = false;
        }
        if (Iron >= 30 && Coal >= 50 && Liquid >= 50)
        {
            Droid3.interactable = true;
        }
        else
        {
            Droid3.interactable = false;
        }
        if (Iron >= 100 && Coal >= 50)
        {
            Droid4.interactable = true;
        }
        else
        {
            Droid4.interactable = false;
        }
        if (Iron >= 150 && Liquid >= 100)
        {
            Droid5.interactable = true;
        }
        else
        {
            Droid5.interactable = false;
        }
        if (NearFactory == false)
        {
            OpenButton.SetActive(false);
            CloseButton.SetActive(false);
            Panel.SetActive(false);
        }
        if (NearFactory == true)
        {
            OpenButton.SetActive(true);
        }
        #endregion
    }
    private void OnTriggerStay(Collider Col)
    {
        if (Col.gameObject.tag == "Player" || Col.gameObject.tag == "Charging")
        {
            NearFactory = true;
            if (DropRes == true)
            {
                Coal += Col.GetComponent<Drone>().InventoryCoal;
                Col.GetComponent<Drone>().InventoryCoal = 0;
                Iron += Col.GetComponent<Drone>().InventoryIron;
                Col.GetComponent<Drone>().InventoryIron = 0;
                Rock += Col.GetComponent<Drone>().InventoryRock;
                Col.GetComponent<Drone>().InventoryRock = 0;
                Liquid += Col.GetComponent<Drone>().InventoryLiquids;
                Col.GetComponent<Drone>().InventoryLiquids = 0;
                DropRes = false;
            }
        }
    }
    #region Buttons
    private void OnTriggerExit(Collider Col)
    {
        NearFactory = false;
    }
    public void OnClickResource()
    {
        DropRes = true;
    }
    public void OnClickDroid_1()
    {
        if (CoolDownBool == false)
        {
            StartCoroutine(CoolDown(Drone1Prefab));
            Iron -= 40;
            Coal -= 30;
            Liquid -= 20;
            CoolDownBool = true;
        }
    }
    public void OnClickDroid_2()
    {
        if (CoolDownBool == false)
        {
            StartCoroutine(CoolDown(Drone2Prefab));
            Iron -= 50;
            Coal -= 30;
            Liquid -= 10;
            CoolDownBool = true;
        }
    }
    public void OnClickDroid_3()
    {
        if (CoolDownBool == false)
        {
            StartCoroutine(CoolDown(Drone3Prefab));
            Iron -= 30;
            Coal -= 50;
            Liquid -= 50;
            CoolDownBool = true;
        }
    }
    public void OnClickDroid_4()
    {
        if (CoolDownBool == false)
        {
            StartCoroutine(CoolDown(Drone4Prefab));
            Iron -= 100;
            Coal -= 50;
            CoolDownBool = true;
        }
    }
    public void OnClickDroid_5()
    {
        if (CoolDownBool == false)
        {
            StartCoroutine(CoolDown(Drone5Prefab));
            Iron -= 150;
            Liquid -= 100;
            CoolDownBool = true;
        }
    }
    #endregion
    IEnumerator CoolDown(GameObject Drone)//добавить очередь на строительство
    {
        LoadingText.text = "0%";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "20%";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "40%";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "60%";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "80%";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "100%";
        Instantiate(Drone, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z - 5f), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        LoadingText.text = "";
        CoolDownBool = false;
    }
}
