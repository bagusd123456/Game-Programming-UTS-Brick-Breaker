using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 lastVelocity;
    private int Score;
    [HideInInspector] public int Lives;
    private bool isStick;
    [HideInInspector] public GameObject[] obstacleList;

    public Text scoreText;
    public Text livesText;
    public GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Vector3.down * 250f);
        Score = 0;
        Lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        obstacleList = GameObject.FindGameObjectsWithTag("Box");
        //CheckObstacles();
        lastVelocity = rb.velocity;
        scoreText.text ="Score: " + Score.ToString();
        livesText.text = "Lives: " + Lives.ToString();

        if (Input.GetKeyDown(KeyCode.Space) && isStick)
        {
            rb.AddForce(Vector3.up * 250f);
            //rb.AddForce(new Vector3(0.2f * 100f, 1 * 250f, 0));
            transform.parent = null;
            isStick = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);
        if (collision.gameObject.CompareTag("Box"))
        {
            Score += 10;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("BottomLine"))
        {
            Lives--;
            ResetBall();
            gameObject.transform.parent = player.transform;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            float corner = (transform.position.x - collision.transform.position.x) * 5f;
            Vector3 arah = new Vector3(corner, rb.velocity.y).normalized;
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(arah * 180 * 2);
        }
    }

    void ResetBall()
    {
        isStick = true;
        transform.localPosition = new Vector3(
            player.transform.position.x, 
            player.transform.position.y + 0.74f, 
            player.transform.position.z); 

        rb.velocity = Vector3.zero;
    }

    

    void CheckLives()
    {
        if(Lives <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
