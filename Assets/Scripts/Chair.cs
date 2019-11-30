using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;

    private bool ready;
    
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
            GameObject characterObject = Instantiate(characterPrefab, new Vector3(2, 0, 0), Quaternion.identity);
            Character character = characterObject.GetComponent<Character>();
        }
    }

    private bool isFacingRight()
    {
        // si esta girada manualmente es que esta mirando a derechas
        return transform.localScale.x < 0;
    }
}
