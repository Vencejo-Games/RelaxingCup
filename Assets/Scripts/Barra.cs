using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barra : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        Debug.Log(canvas);
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hola colision");
        if (collision.tag == "Player") {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.enabled = false;
        }
    }
}
