using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private Game game;

    private bool ready;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // indica que la silla está libre
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            Debug.Log("Silla ready");
            
            // Obtenemos un prefab de cliente aleatorio
            GameObject clientPrefab = game.GetClientPrefab();
            
            // Instanciamos en la posicion de inicio de clientes fuera de pantalla
            GameObject characterObject = Instantiate(clientPrefab, game.clientStartPoint, Quaternion.identity);
            Client client = characterObject.GetComponent<Client>();
            client.start.position = game.clientStartPoint;
            client.end.position = transform.position;

            // marcar silla como ocupada
            ready = false;
        }
    }

    private bool isFacingRight()
    {
        // si esta girada manualmente es que esta mirando a derechas
        return transform.localScale.x < 0;
    }
}
