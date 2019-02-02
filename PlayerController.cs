using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text allText;
    public Text livesText;

    private Rigidbody rb;
    private int count;
    private int score;
    private int lives;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        score = 0;
        SetAllText();
        lives = 3;
        SetLivesText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
            score = score + 1; // I added this code to track the score and count separately.
            SetAllText();

        }
        if (other.gameObject.CompareTag("Enemy Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            lives = lives - 1; // this removes 1 from the lives
            SetLivesText();
        }
        if (lives == 0) //this number should be equal to original value lives - lives
        {
            Destroy(this.gameObject);
        }

        if (count == 12) //this number should be equal to the number of yellow pickups on the first playfield
        {
            transform.position = new Vector3(23.5f, transform.position.y, 8.0f);
        }
        
    }
    
    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            winText.text = "You Lose!";
        }
    }
    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 23)
        {
            winText.text = "You Lose!";
        }
    }
    void SetAllText ()
    {
        allText.text = "Score: " + score.ToString();
        if (score >= 20)
        {
            winText.text = "You Win!";
        }
    }
}
