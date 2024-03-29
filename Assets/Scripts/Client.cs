﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : Character
{
    [SerializeField] private string[] texts;
    [SerializeField] private int identificador;

    private Coffee coffee;
    public Chair chair;

    public int idCoffee;

    // Componentes del Character
    private Canvas canvas1;
   
    private Character playerController;

    public Vector3 tablePosition;

    private bool clientWithCoffee;

    // guardamos referencia del coffee para poder destruirlo cuando se vaya
    private GameObject coffeInTable;
    
    protected override void Awake()
    {
        base.Awake();
        canvas1 = GetComponentInChildren<Canvas>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canvas1.enabled = false;
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
        game.PlayClientSound();
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
                    game.PlayYupiSound();
                    // guardamos referencia del coffee para poder destruirlo cuando se vaya
                    coffeInTable = coffeeObj;
                    // dejamos huerfano al coffee para poder ponerlo en la mesa
                    coffeeObj.transform.parent = null;
                    // lo colocamos en la posicion guardada de la mesa segun silla (coge z -4 de los puntos)
                    coffeeObj.transform.position = tablePosition;
                    // el camarero ya no tiene cafe en la bandeja
                    playerController.withCoffee = false;

                    // el cliente tiene el coffee
                    clientWithCoffee = true;

                    // TODO Lanzar Timer para levantarse de la silla
                    StartCoroutine(Pirarse());
                }
                else
                {
                    game.PlayErrorSound();
                    // mostrar deseo
                    StartCoroutine(ActivarDeseo());
                }
            }
            else
            {
                // mostrar deseo
                StartCoroutine(ActivarDeseo());
            }
        }
        else
        {
            StartCoroutine(ActivarTexto());

        }
        
    }

    IEnumerator ActivarDeseo()
    {
        // this object was clicked
        canvas1.enabled = true;
        canvas1.transform.GetChild(0).gameObject.SetActive(true); //bocadillo 
        canvas1.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);  //foto cafe
        canvas1.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = coffee.getSprite(idCoffee);
        canvas1.transform.GetChild(1).gameObject.SetActive(false);  //texto
        yield return new WaitForSeconds(4);
        canvas1.enabled = false;
    }

    IEnumerator ActivarTexto()
    {
        // this object was clicked
        canvas1.enabled = true;
        canvas1.transform.GetChild(1).gameObject.SetActive(true); //bocadillo 
        canvas1.transform.GetChild(0).gameObject.SetActive(false);  //foto cafe
        canvas1.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);  //texto
        canvas1.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = chooseRandomText();
        yield return new WaitForSeconds(6);
        canvas1.enabled = false;
    }

    private string chooseRandomText() {
        return texts[Random.Range(0, texts.Length)];
    }

    IEnumerator Pirarse()
    {
        // Tiempo de espera hasta irse
        yield return new WaitForSeconds(20);
        // Desactivar bocadillo
        canvas1.transform.GetChild(1).gameObject.SetActive(false);
        // Destruir su coffee en la mesa
        Destroy(coffeInTable);
        // Sprite de andar
        spriteRenderer.sprite = walkSprite;
        if (!flip)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            // flipear bocadillo para los que hemos girado
            Canvas c = transform.GetChild(0).GetComponent<Canvas>();
            RectTransform t = c.GetComponent<RectTransform>();
            t.rotation = new Quaternion(t.rotation.x, 180.0f, t.rotation.z, 1);
        }
        
        // Que se salga fuera de pantalla (z -4.5 para client walking)
        transform.position = new Vector3(transform.position.x, transform.position.y, -4.5f);
        end = game.clientStartPoint;
        // Esperamos a que le de tiempo a salir
        yield return new WaitForSeconds(5);
        // Liberamos la silla
        chair.Free();
        // Margen de tiempo hasta que entre otro cliente
        yield return new WaitForSeconds(5);
        // Avisamos al game manager para que libere el client
        game.LiberaCliente(identificador);
        // GG!
        Destroy(this.gameObject);
    }

}
