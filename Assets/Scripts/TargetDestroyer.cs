using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyer : MonoBehaviour
{
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            //transform.position = new Vector3(0, -2000, 0);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.CompareTag("Player") || Col.CompareTag("Charging"))
        {
            Col.gameObject.GetComponent<Drone>().TargetSelectedLocal = false;
        }
    }
}
