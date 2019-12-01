using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] public Vector3 clientStartPoint;

    [SerializeField] private Coffee[] coffees;

    [SerializeField] public GameObject[] clients;

    private bool[] clientLock;

    // Start is called before the first frame update
    void Start()
    {
        clientLock = new bool[clients.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Coffee Deseo()
    {
        // devolvemos un cafe aleatorio cuando un cliente tiene un deseo
        Coffee coffee = Instantiate(coffees[0], clientStartPoint, Quaternion.identity); ;
        coffee.id = Random.Range(0, coffee.animations.Length);
        return coffee;
    }

    public GameObject GetClient()
    {
        bool found = false;
        while (!found)
        {
            int i = Random.Range(0, clients.Length);
            if (!clientLock[i])
            {
                found = true;
                clientLock[i] = true;
                return clients[i];
                
            }
        }
        return clients[0];
    }

    public void LiberaCliente(int i)
    {
        clientLock[i] = false;
    }

}
