using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Sprite walkSprite;
    [SerializeField] public Sprite seatedSprite;

    [SerializeField] public Transform start;
    [SerializeField] public Transform end;

    private float walkSpeed = 0.5f;
    private float rotationSpeed = 100f;
    private float jumpSpeed = 0.1f;
    private float walkRotation = 3.0f;
    private float walkJump = 0.005f;

    private Quaternion targetRotation;
    private float targetJump;
   
    // Componentes del Character
    private SpriteRenderer spriteRenderer;
    private GameObject sombra;

    protected Game game;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        game = FindObjectOfType<Game>();
        sombra = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // ponemos al personaje en la posicion inicial
        transform.position = start.position;
        targetRotation = Quaternion.Euler(0, 0, walkRotation);
        targetJump = transform.position.y + walkJump;

        spriteRenderer.sprite = walkSprite;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!playerArrived())
        {
            UpdatePosition();
            UpdateRotation();
            HasArrived();
        }
    }

    private void UpdatePosition()
    {
        // desplazar la transformada hacia el punto destino
        transform.position = Vector3.MoveTowards(transform.position, end.position, walkSpeed * Time.deltaTime);

        // saltito south park
        if (jumpSpeed > 0 && transform.position.y >= targetJump
            || jumpSpeed < 0 && transform.position.y <= targetJump)
        {
            jumpSpeed *= -1.0f;
            walkJump *= -1.0f;
            targetJump = targetJump + (walkJump * 2);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + jumpSpeed * Time.deltaTime, transform.position.z);
        sombra.transform.position = new Vector3(transform.position.x, sombra.transform.position.y, transform.position.z);
    }


    public void setFinalPosition(Transform t) {
        end.position = t.position;
    }

    private void UpdateRotation()
    {
        // si hemos llegado a la rotacion objetivo cambiamos el target
        if (transform.rotation.Equals(targetRotation))
        {
            walkRotation *= -1.0f;
            targetRotation = Quaternion.Euler(0, 0, walkRotation);
        }
        // Rotar hacia el target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HasArrived()
    {
        // si hemos llegado a la silla cambiamos el sprite por el de sentado
        if (playerArrived())
        {
            transform.position = end.position;
            transform.rotation = end.rotation;
            spriteRenderer.sprite = seatedSprite;
            
        }
    }

    private bool playerArrived() {
        return (transform.position.x == end.position.x);
    }
}
