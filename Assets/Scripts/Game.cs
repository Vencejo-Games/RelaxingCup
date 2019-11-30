using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] public Vector3 ClientStartPoint;

    [SerializeField] private Coffee[] coffees;

    [SerializeField] public Sprite[] walkSprites;
    [SerializeField] public Sprite[] seatSprites;

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
