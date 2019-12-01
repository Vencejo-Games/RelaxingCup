using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private bool facingRight;

    [SerializeField] private GameObject table;

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
            // Obtenemos un prefab de cliente aleatorio
            GameObject clientPrefab = game.GetClient();

            // Instanciamos en la posicion de inicio de clientes fuera de pantalla
            GameObject characterObject = Instantiate(clientPrefab, game.clientStartPoint, Quaternion.identity);

            // Configurar los parámetros del cliente
            Client client = characterObject.GetComponent<Client>();
            client.start = game.clientStartPoint;
            // clients walking a z -4.5 para que pasen por delante de sillas y mesas
            client.end = transform.position;
            client.end.z = -4.5f;
            client.tablePosition = table.transform.position;
            client.chair = this;

            // hacemos flip del cliente si la silla mira hacia la derecha
            if (facingRight) { client.flip = true; }

            // marcar silla como ocupada
            ready = false;
        }
    }

    public void Free()
    {
        ready = true;
    }

}
