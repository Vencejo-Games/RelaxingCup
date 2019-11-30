using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : Character
{
    // Componentes del Character
    private Canvas canvas;

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInChildren<Canvas>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canvas.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    void OnMouseDown()
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
