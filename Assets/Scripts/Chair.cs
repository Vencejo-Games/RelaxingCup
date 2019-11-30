using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private Game game;

    private bool ready;

    [SerializeField] private bool facingRight;

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
            // Obtenemos un prefab de cliente aleatorio
            GameObject clientPrefab = game.GetClient();
            //client.start = game.clientStartPoint;
            //client.end = transform.position;

            // Instanciamos en la posicion de inicio de clientes fuera de pantalla
            GameObject characterObject = Instantiate(clientPrefab, game.clientStartPoint, Quaternion.identity);
            Client client = characterObject.GetComponent<Client>();
            client.start = game.clientStartPoint;
            client.end = transform.position;
            client.end.z = -1;
            if (facingRight) { client.flip = true; }

            // marcar silla como ocupada
            ready = false;
        }
    }

}
