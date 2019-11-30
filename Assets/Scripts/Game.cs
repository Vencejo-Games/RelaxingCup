using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAme : MonoBehaviour
{
    [SerializeField] private Coffee[] coffees;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Coffee Deseo()
    {
        // devolvemos un cafe aleatorio cuando un cliente tiene un deseo
        return coffees[Random.Range(0, coffees.Length)];
    }
}
