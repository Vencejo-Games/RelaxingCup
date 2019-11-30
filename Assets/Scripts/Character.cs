using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Sprite walkSprite;
    [SerializeField] private Sprite seatedSprite;

    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float jumpSpeed = 0.1f;

    [SerializeField] private float walkRotation = 3.0f;
    [SerializeField] private float walkJump = 0.005f;

    private Quaternion targetRotation;
    private float targetJump;
    private bool hasArrived;

    // Componentes del Character
    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (!hasArrived)
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
        if (transform.position.x == end.position.x)
        {
            transform.position = end.position;
            transform.rotation = end.rotation;
            spriteRenderer.sprite = seatedSprite;
            hasArrived = true;
        }
    }

}
