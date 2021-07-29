using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeHelper : MonoBehaviour
{
    private void OnTriggerStay(Collider Col)
    {
        if (Col.gameObject.tag == "Building")
        {
            Destroy(Col.gameObject);
        }
    }
}
