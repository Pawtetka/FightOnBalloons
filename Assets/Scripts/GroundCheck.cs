using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool onGround;
    [SerializeField] private GameObject dust;
    public bool OnGround { get { return onGround; } }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            if (dust != null)
            {
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("Hit4");
                }
                dust.SetActive(true);
                Invoke("DisableDust", 0.5f);
            }
        }
    }
    private void DisableDust()
    {
        dust.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}
