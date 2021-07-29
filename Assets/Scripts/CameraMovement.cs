using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class CameraMovement : MonoBehaviour
{
    public static int Difficult = 2;
    public List<GameObject> Droids = new List<GameObject>();
    [SerializeField]
    private RaycastHit hit;
    public Transform LookAt;
    public float Speed = 10;
    private Rigidbody rb;
    private Camera mainCamera;
    public GameObject Target, Base, DroidPrefab;
    public static bool CoolDown;
    private Coroutine A;
    void Start()
    {
        if(Difficult == 1)
        {
            Instantiate(DroidPrefab, new Vector3(transform.position.x + 3f, transform.position.y + .6f, transform.position.z + 3f), Quaternion.identity);
            Instantiate(DroidPrefab, new Vector3(transform.position.x + 2f, transform.position.y + .6f, transform.position.z + 2f), Quaternion.identity);
            Instantiate(DroidPrefab, new Vector3(transform.position.x + 1f, transform.position.y + .6f, transform.position.z + 1f), Quaternion.identity);
        }
        if(Difficult == 2)
        {
            Instantiate(DroidPrefab, new Vector3(transform.position.x + 2f, transform.position.y + .6f, transform.position.z + 2f), Quaternion.identity);
            Instantiate(DroidPrefab, new Vector3(transform.position.x + 1f, transform.position.y + .6f, transform.position.z + 1f), Quaternion.identity);
        }
        if(Difficult == 3)
        {
            Instantiate(DroidPrefab, new Vector3(transform.position.x + 1f, transform.position.y + .6f, transform.position.z + 1f), Quaternion.identity);
        }
        CoolDown = false;
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        mainCamera = Camera.main;
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))  
        {
            FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
            //SaveLoad.Save();
            SceneManager.LoadScene(0);
        }
        //Movement
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        Physics.Raycast(downRay, out hit);
        Debug.DrawLine(downRay.origin, hit.point, Color.red);
        if (hit.distance < 0.1f)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y+.001f, transform.position.z);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, .5f, gameObject.GetComponent<Rigidbody>().velocity.z);
        }
        else if (hit.distance > 0.1f)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, -.5f, gameObject.GetComponent<Rigidbody>().velocity.z);
        }
        else
        {
            //gameObject.GetComponent<Rigidbody>().velocity.y == hit.distance;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * Speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * Speed);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.drag = 10;
        }
        else
        {
            rb.drag = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Drone.TimeSpeed = 5f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Drone.TimeSpeed = 10f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Drone.TimeSpeed = 15f;
        }
        //Rotation
        float rotationX = 0;
        if(Input.GetKey(KeyCode.Q))
        {
            rotationX++;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationX--;
        }
        transform.Rotate(0, rotationX, 0);
        //Selecting Drone 
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray Selecter = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(Selecter, out hit))
            {
                 Debug.DrawLine(Selecter.origin, Selecter.direction, Color.red);
                if (hit.collider.GetComponent<Rigidbody>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Drone>().DroneSelected == false && CoolDown==false)
                    {
                        hit.collider.gameObject.GetComponent<Drone>().DroneSelected = true;
                        Droids.Add(hit.collider.gameObject);
                        CoolDown = true;
                        A = StartCoroutine(CoolDownCounter());
                    }
                    else if (hit.collider.gameObject.GetComponent<Drone>().DroneSelected == true && CoolDown == false)
                    {
                        hit.collider.gameObject.GetComponent<Drone>().DroneSelected = false;
                        CoolDown = true;
                        A = StartCoroutine(CoolDownCounter());
                    }
                }
            }
        }
        //Selecting Target
        if (Input.GetKey(KeyCode.Mouse0) && Drone.TargetSelected == false && !Input.GetKey(KeyCode.LeftControl))
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                Target.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                Target.SetActive(true);
                Drone.TargetSelected = true;
                Drone.NeedToCorrect = true;
            }
        }
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftControl))
        {
            Target.SetActive(false);
            Drone.TargetSelected = false;
            Drone.NeedToCorrect = true;
        }
    }   
    IEnumerator CoolDownCounter()
    {
        yield return new WaitForSeconds(0.1f);
        CoolDown = false;
    }
    public void OnClickBase()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(Base.transform.position.x, transform.position.y, Base.transform.position.z);
    }
}
