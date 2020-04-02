using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float hops;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private bool grounded;
    private bool doubleJumped;
    private int count;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
        doubleJumped = false;
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        Vector3 jump = new Vector3(0, 1, 0);

        if ((grounded || !doubleJumped) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump * hops, ForceMode.Impulse);
            if (!grounded)
            {
                doubleJumped = true;
            }
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            doubleJumped = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
