using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool isLiquid = false, isRock = false, isCoal = false, isIron = false;
    private float ResourceForInventory = 150;
    private Coroutine A;
    private void Update()
    {
        if(ResourceForInventory <= 0)
        {
            StopCoroutine(A);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            if (ResourceForInventory > 0)
            {
                A = StartCoroutine(V());
                IEnumerator V()
                {
                    if (isLiquid == false && Col.gameObject.GetComponent<Drone>().AllHardInventory < 500)
                    {
                        ResourceForInventory--;
                        if (isCoal == true) Col.gameObject.GetComponent<Drone>().InventoryCoal++;
                        if (isRock == true) Col.gameObject.GetComponent<Drone>().InventoryRock++;
                        if (isIron == true) Col.gameObject.GetComponent<Drone>().InventoryIron++;
                    }
                    else if(isLiquid == true && Col.gameObject.GetComponent<Drone>().InventoryLiquids < 300)
                    {
                        ResourceForInventory--;
                        Col.gameObject.GetComponent<Drone>().InventoryLiquids++;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        else if(Col.gameObject.tag == "Charging")
        {
            if (ResourceForInventory > 0)
            {
                A = StartCoroutine(V());
                IEnumerator V()
                {
                    if (isLiquid == false && Col.gameObject.GetComponent<Drone>().AllHardInventory < 500)
                    {
                        ResourceForInventory--;
                        Col.gameObject.GetComponent<Drone>().InventoryIron++;
                    }
                    if (isLiquid == false && Col.gameObject.GetComponent<Drone>().InventoryLiquids < 300)
                    {
                        ResourceForInventory--;
                        Col.gameObject.GetComponent<Drone>().InventoryLiquids++;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else if(ResourceForInventory <= 0)
            {
                StopCoroutine(A);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider Col)
    {
        if(Col.gameObject.tag == "Player") StopCoroutine(A);
        else if (Col.gameObject.tag == "Charging") StopCoroutine(A);
    }
}
