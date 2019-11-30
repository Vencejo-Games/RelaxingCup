using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : Character
{
    [SerializeField] private string[] texts;

    private Coffee coffee;

    // Componentes del Character
    private Canvas canvas;
    
    private Character playerController;

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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    void OnMouseDown()
    {
        playerController.setFinalPosition(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ActivarBocadillo());
    }

    IEnumerator ActivarBocadillo()
    {
        // this object was clicked
        canvas.enabled = true;
        yield return new WaitForSeconds(3);
        canvas.enabled = false;
    }

}
