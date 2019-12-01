using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterGaleria : MonoBehaviour
{
    [SerializeField] public Sprite sprite;

    [SerializeField] public int id;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.GetInt("" + id) != 0)
        {
            spriteRenderer.sprite = sprite;
        }
    }

}
