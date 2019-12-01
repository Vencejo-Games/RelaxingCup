using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCoffees : MonoBehaviour
{
    public GameObject coffee;

    private Character player;

    private Transform spawnPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

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
        // Destruir café anterior en caso necesario
        DestroyPreviousCoffee();
        GameObject obj = Instantiate(coffee, spawnPosition);
        obj.GetComponent<Coffee>().id = 1;
        playerWithCoffee(obj);
    }

    public void GenerateCoffeeConLeche()
    {
        DestroyPreviousCoffee();
        GameObject obj = Instantiate(coffee, spawnPosition);
        obj.GetComponent<Coffee>().id = 0;
        playerWithCoffee(obj);
    }

    public void GenerateColacao()
    {
        DestroyPreviousCoffee();
        GameObject obj = Instantiate(coffee, spawnPosition);
        obj.GetComponent<Coffee>().id = 2;
        playerWithCoffee(obj);
    }

    // marcamos que el jugador lleva coffee en la bandeja
    private void playerWithCoffee(GameObject newCoffee)
    {
        // modificamos z -6 para coffee en la bandeja
        newCoffee.transform.position = new Vector3(newCoffee.transform.position.x, newCoffee.transform.position.y, -6);
        // guardar referencia al coffee creado para destruirlo
        player.plateCoffeeObject = newCoffee;
        player.withCoffee = true;
    }

    private void DestroyPreviousCoffee()
    {
        if (player.withCoffee)
        {
            Destroy(player.plateCoffeeObject);
            player.withCoffee = false;
        }
    }

}
