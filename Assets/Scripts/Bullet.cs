using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy());
    }
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player" || Col.gameObject.tag == "Charging")
        {
            Col.gameObject.GetComponent<Drone>().Health -= 10 * CameraMovement.Difficult; 
            Destroy(gameObject);
            //* SaveLoad.current.Difficult
        }
        else if(Col.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(50f);
        Destroy(gameObject);
    }

}
