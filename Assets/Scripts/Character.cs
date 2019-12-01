using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Sprite walkSprite;
    [SerializeField] public Sprite seatedSprite;

    [SerializeField] public Vector3 start;
    [SerializeField] public Vector3 end;

    public bool flip;

    private float walkSpeed = 0.5f;
    private float rotationSpeed = 100f;
    private float jumpSpeed = 0.1f;
    private float walkRotation = 3.0f;
    private float walkJump = 0.005f;

    private Quaternion targetRotation;
    private float targetJump;
   
    // Componentes del Character
    protected SpriteRenderer spriteRenderer;
    private GameObject sombra;

    protected Game game;

    public GameObject plateCoffeeObject;
    public bool withCoffee;

    [SerializeField] private Vector3 offset = new Vector3(-0.2f, -0.1f, 0f);

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
        transform.position = start;
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
        transform.position = Vector3.MoveTowards(transform.position, end, walkSpeed * Time.deltaTime);

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


    public void setFinalPosition(Vector3 endPosition) {
        end = endPosition + offset;
        // witer siempre en z -5
        end.z = -5;
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
            transform.position = end;
            // si es cliente lo ponemos en z -2 para que se quede detrás de la mesa
            if(gameObject.tag != "Player")
            {
                transform.position = new Vector3(end.x, end.y, -2);
                transform.position = new Vector3(end.x, end.y, -2);
            }
            transform.rotation = Quaternion.identity;
            spriteRenderer.sprite = seatedSprite;
            if (flip)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

                // flipear el canvas del bocadillo
                Canvas c = transform.GetChild(0).GetComponent<Canvas>();
                RectTransform t = c.GetComponent<RectTransform>();
                t.rotation = new Quaternion(t.rotation.x, 180.0f, t.rotation.z, 1);
            }
        }
    }

    private bool playerArrived() {
        return (transform.position.x == end.x);
    }
}
