using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : Character
{
    [SerializeField] private string[] texts;

    private Coffee coffee;
    public Chair chair;

    public int idCoffee;

    // Componentes del Character
    private Canvas canvas;
    
    private Character playerController;

    public Vector3 tablePosition;

    private bool clientWithCoffee;

    // guardamos referencia del coffee para poder destruirlo cuando se vaya
    private GameObject coffeInTable;

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInChildren<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canvas.enabled = false;
        coffee = game.Deseo();
        idCoffee = coffee.id;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    void OnMouseDown()
    {
        playerController.setFinalPosition(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!clientWithCoffee)
        {
            // Si camarero lleva coffee intenta darselo al cliente
            if (playerController.withCoffee)
            {
                GameObject coffeeObj = playerController.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                Coffee currentCoffee = coffeeObj.GetComponent<Coffee>();
                
                // Comprobar que es el deseo del cliente
                if (currentCoffee.id == coffee.id)
                {
                    // guardamos referencia del coffee para poder destruirlo cuando se vaya
                    coffeInTable = coffeeObj;
                    // dejamos huerfano al coffee para poder ponerlo en la mesa
                    coffeeObj.transform.parent = null;
                    // lo colocamos en la posicion guardada de la mesa segun silla
                    coffeeObj.transform.position = tablePosition;
                    // el camarero ya no tiene cafe en la bandeja
                    playerController.withCoffee = false;

                    // el cliente tiene el coffee
                    clientWithCoffee = true;

                    // TODO Lanzar Timer para levantarse de la silla
                    StartCoroutine(Pirarse());
                }

            }
            else
            {
                // mostrar deseo
                StartCoroutine(ActivarBocadillo());
            }
        }
        else
        {
            // TODO Mientras no se va mostrar texto
        }

        
    }

    IEnumerator ActivarBocadillo()
    {
        // this object was clicked
        canvas.enabled = true;
        yield return new WaitForSeconds(3);
        canvas.enabled = false;
    }

    IEnumerator Pirarse()
    {
        // Tiempo de espera hasta irse
        yield return new WaitForSeconds(5);
        // Destruir su coffee en la mesa
        Destroy(coffeInTable);
        // Sprite de andar
        spriteRenderer.sprite = walkSprite;
        // Que se salga fuera de pantalla
        end = game.clientStartPoint;
        // Esperamos a que le de tiempo a salir
        yield return new WaitForSeconds(5);
        // Liberamos la silla
        chair.Free();
        // Margen de tiempo hasta que entre otro cliente
        yield return new WaitForSeconds(2);
        // GG!
        Destroy(this.gameObject);
    }

}
