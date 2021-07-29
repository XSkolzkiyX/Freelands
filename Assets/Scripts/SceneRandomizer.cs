using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRandomizer : MonoBehaviour
{
    public GameObject Rocks, BigRocks, ResourceIron, ResourceCoal, ResourceLiquid, Turrel;
    public float[] RockX, RockY, RockZ;
    public int RockRange;
    /*private void Start()
    {
        RockRange = Random.Range(500, 2500);
        RockX = new float[RockRange];
        RockY = new float[RockRange];
        RockZ = new float[RockRange];
        for (int i = 0; i < RockRange; i++)
        {
            var terrain = gameObject.GetComponent<Terrain>();
            var x = Random.Range(-800.0f, 800.0f);
            var z = Random.Range(-800.0f, 800.0f);
            var y = terrain.SampleHeight(new Vector3(x, 0.1f, z));
            Vector3 position = new Vector3(0, 0, 0);
            Quaternion rotation = new Quaternion(0, Random.Range(0.0f, 1.0f), 0, 1);
            var go = Instantiate(Rocks, position, rotation);
            go.transform.localPosition = new Vector3(x, y, z);
            RockX[i] = go.transform.position.x;
            RockY[i] = go.transform.position.y;
            RockZ[i] = go.transform.position.z;
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        //Rocks
        Generate(Rocks, 500, 2500);
        //Big Rocks
        Generate(BigRocks, 200, 1500);
        //ResIron
        Generate(ResourceIron, 100, 800);
        //Coal
        Generate(ResourceCoal, 100, 800);
        //ResLiquid
        Generate(ResourceLiquid, 100, 800);
        //Turrels
        Generate(Turrel, 100, 800);
    }
    void Generate(GameObject prefab, int From, int To)
    {
        for (int i = 0; i < Random.Range(From, To); i++)
        {
            var terrain = GetComponent<Terrain>();
            var x = Random.Range(-800.0f, 800.0f);
            var z = Random.Range(-800.0f, 800.0f);
            var y = terrain.SampleHeight(new Vector3(x, 0.1f, z));
            Vector3 position = new Vector3(0, 0, 0);
            //Vector3 position = new Vector3(Random.Range(-200.0f, 200.0f), 0, Random.Range(-300.0f, 300.0f));
            Quaternion rotation = new Quaternion(0, Random.Range(0.0f, 1.0f), 0, 1);
            var go = Instantiate(prefab, position, rotation);
            go.transform.localPosition = new Vector3(x, y, z);
        }
    }
}
