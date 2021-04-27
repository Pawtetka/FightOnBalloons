using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerAppearing : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }
}
