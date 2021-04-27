using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed;
    public float airSpeed;
    public float jumpForce;
    public Joystick joystick;
    public bool ButtonDown { get; set; }
    private Animator _anim;
    
    private float _horizontalMove = 0f;
    private Rigidbody2D _rb;
    [SerializeField] private GameObject balloons;
    [SerializeField] private float inflateTime;

    enum State { Play, Falling, Inflating}
    private State state;

    private bool facingRight = true;
    private bool onGround;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int OnGround = Animator.StringToHash("OnGround");

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        state = State.Inflating;
        Invoke("StartMove", inflateTime);
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update ()
    {
        _anim.SetFloat(Speed, Math.Abs(joystick.Horizontal));
        if (onGround)
        {
            _anim.SetBool(OnGround, true);
            _rb.drag = 3;
        }
        else
        {
            _anim.SetBool(OnGround, false);
            _rb.drag = 0.5f;
        }
        if (state == State.Play)
        {
            _horizontalMove = joystick.Horizontal * runSpeed;
            if (!CheckBalloons())
            {
                state = State.Falling;
            }
        }
    }

    private void StartMove()
    {
        for (int i = 0; i < balloons.transform.childCount; i++)
        {
            balloons.transform.GetChild(i).gameObject.SetActive(true);
        }
        state = State.Play;
    }
    
    private bool CheckBalloons()
    {
        for (int i = 0; i < balloons.transform.childCount; i++)
        {
            if (balloons.transform.GetChild(i).gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void VerticalMovement()
    {
        if (state == State.Play)
        {
            _horizontalMove = joystick.Horizontal * airSpeed;
            _rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime));
        }
    }

    private void FixedUpdate ()
    {
        if (joystick.Horizontal != 0 && !onGround && ButtonDown && SpeedLimit())
        {
            _rb.AddForce(new Vector2(joystick.Horizontal * airSpeed * Time.deltaTime, 0));
        }
        else if (onGround && SpeedLimit())
        {
            _rb.AddForce(new Vector2(joystick.Horizontal * runSpeed * Time.deltaTime, 0));
        }
        if (_horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        else if (_horizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && state == State.Falling)
        {
            state = State.Inflating;
            _horizontalMove = 0;
            Invoke("StartMove", inflateTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private bool SpeedLimit()
    {
        if (Math.Abs(_rb.velocity.x) < Math.Abs(_horizontalMove * Time.deltaTime))
        {
            return true;
        }
        else if ((_rb.velocity.x > 0 && _horizontalMove < 0) || (_rb.velocity.x < 0 && _horizontalMove > 0))
        {
            return true;
        }
        return false;
    }
}
