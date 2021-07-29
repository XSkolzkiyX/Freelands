using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private GameObject Camera;
    public static bool TargetSelected = false, NeedToCorrect = false;
    public bool DroneSelected = false, TargetSelectedLocal;
    private Vector3 TargetPoint, Balance;
    private Transform LookAtTransform;
    public GameObject LookAt;
    public float InventoryLiquids, InventoryIron, InventoryCoal, InventoryRock, AllHardInventory, Health;
    private Rigidbody rb;
    public int Energy;
    private Coroutine A;
    public static float TimeSpeed = 5f;
    private float Y;
    void Start()
    {
        TargetSelectedLocal = TargetSelected;
        //Debug.Log(SaveLoad.current.Difficult);
        Health = 150f;
        Camera = GameObject.Find("Axis");
        LookAt = gameObject;
        Y = transform.position.y;
        A = StartCoroutine(EnergySpending());
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(NeedToCorrect == true)
        {
            TargetSelectedLocal = TargetSelected;
            NeedToCorrect = false;
        }
        if (Health > 0)
        {
            //Rework?
            /*if (Input.GetKey(KeyCode.Mouse0) && TargetSelected == false && !Input.GetKey(KeyCode.LeftControl))
            {
                LookAt = GameObject.Find("Target");
            }
            if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftControl))
            {
                LookAt = gameObject;
            }*/
            if(TargetSelected == true)
            {
                LookAt = GameObject.Find("Target");
            }
            if(TargetSelected == false)
            {
                LookAt = gameObject;
            }
            LookAtTransform = LookAt.GetComponent<Transform>();
            AllHardInventory = InventoryRock + InventoryCoal + InventoryIron;
            if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.LeftControl))
            {
                DroneSelected = false;
                TargetSelected = false;
            }
            if (Energy > 9000)
            {
                Energy = 9000;
            }
            if (Energy <= 0)
            {
                rb.constraints = (0);
                rb.useGravity = true;
                StopCoroutine(A);//?
                Energy = 0;
            }
            else
            {
                RaycastHit hit;
                Ray downRay = new Ray(transform.position, -Vector3.up);
                Physics.Raycast(downRay, out hit);
                Debug.DrawLine(downRay.origin, hit.point, Color.red);
                if (hit.distance < 0.6f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y+.001f, transform.position.z);
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 1f, 0);
                    //Debug.Log("<.6"+hit.distance);
                }
                else if(hit.distance > 0.6f)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -.5f, 0);
                    //Debug.Log(">.6"+hit.distance);
                }
                else
                {
                    //Debug.Log("else"+hit.distance);
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); ;
                }
                rb.useGravity = false;
                if (DroneSelected == true)
                {
                    if (TargetSelected == true)
                    {
                        TargetPoint = new Vector3(LookAtTransform.transform.position.x, transform.position.y, LookAtTransform.transform.position.z);
                        MoveDroid();
                    }
                    else
                    {
                        rb.AddForce(transform.forward * 0);
                        rb.drag = 10;
                    }
                }
            }
        }
        else
        {
            if (Health <= 0) Health = 0;
            rb.constraints = (0);
            rb.useGravity = true;
            StopCoroutine(A);//?
            Energy = 0;
            InventoryIron = 0;
            InventoryRock = 0;
            InventoryCoal = 0;
            InventoryLiquids = 0;
        }
    }
    void MoveDroid()
    {
        transform.LookAt(TargetPoint);
        transform.position = Vector3.MoveTowards(transform.position, TargetPoint, Time.deltaTime * TimeSpeed);
    }
    public IEnumerator EnergySpending()
    {
        while (true)
        {
            if (gameObject.tag == "Charging")
            {
                yield return new WaitForSeconds(1f);
                Energy += (CameraMovement.Difficult * (int)TimeSpeed);
            }
            else
            {
                yield return new WaitForSeconds(4f);
                Energy -= (CameraMovement.Difficult*(int)TimeSpeed);
            }
        }
    }
}