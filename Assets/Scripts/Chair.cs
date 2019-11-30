using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;

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
            // Instantiate at position (0, 0, 0) and zero rotation.
            GameObject characterObject = Instantiate(clientPrefab, game.ClientStartPoint, Quaternion.identity);
            Client client = characterObject.GetComponent<Client>();
        }
    }

    private bool isFacingRight()
    {
        // si esta girada manualmente es que esta mirando a derechas
        return transform.localScale.x < 0;
    }
}
