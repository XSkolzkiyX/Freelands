using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    private bool Started = false;
    private GameObject Drone;
    private Coroutine A;
    public Rigidbody BulletPrefab;
    private void FixedUpdate()
    {
        if (Started == true)
        {
            if (Drone.GetComponent<Drone>().Health <= 0 || Drone.GetComponent<Drone>().Energy <= 0)
            {
                StopCoroutine(A);
                Started = false;
            }
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            Transform Correct = gameObject.transform;
            Rigidbody Bullet;
            Bullet = Instantiate(BulletPrefab, Correct.position, transform.rotation);
            transform.LookAt(Drone.transform);
            transform.Rotate(0, 180, 0);
            Bullet.transform.Translate(0, 2.1f, 0);
            Bullet.velocity = transform.TransformDirection(Vector3.forward * -6);
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player" || Col.gameObject.tag == "Charging")
        {
            if (Col.gameObject.GetComponent<Drone>().Health > 0 || Col.gameObject.GetComponent<Drone>().Energy > 0)
            {
                Drone = Col.gameObject;
                A = StartCoroutine(Shoot());
                Started = true;
            }
        }
    }
    private void OnTriggerExit(Collider Col)
    {
        //if (Col.gameObject.tag == "Player" || Col.gameObject.tag == "Charging")
        StopCoroutine(A);
        Started = false;
    }
}
