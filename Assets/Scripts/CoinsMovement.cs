using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 10, 0), moveSpeed * Time.deltaTime);
    }

}
