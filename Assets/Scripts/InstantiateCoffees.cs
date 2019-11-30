using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCoffees : MonoBehaviour
{
    public GameObject coffee;

    private Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCoffeeSolo()
    {
        GameObject obj = Instantiate(coffee, spawnPosition);
        obj.GetComponent<Coffee>().id = 1;
        
    }

    public void GenerateCoffeeConLeche()
    {
        GameObject obj = Instantiate(coffee, spawnPosition);
        obj.GetComponent<Coffee>().id = 0;
    }

    public void GenerateColacao()
    {
        GameObject obj = Instantiate(coffee, spawnPosition);
        obj.GetComponent<Coffee>().id = 2;
    }
}
