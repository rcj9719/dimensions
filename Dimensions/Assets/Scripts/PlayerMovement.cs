using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;

    public float speed = 5;
    public float scaleSpeed = 0.1f;
    [SerializeField] Rigidbody rb;
    AudioSource audioSource;

    float horizontalInput;
    //[SerializeField] float horizontalMultiplier = 1.5f;

    float verticalInput = 0.0f;
    float xScaleInput = 0.0f;
    float yScaleInput = 0.0f;

    public float speedIncreasePerPoint = 0.01f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;// * horizontalMultiplier;
        Vector3 verticalMove = transform.up * verticalInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove + horizontalMove + verticalMove);


        float xScale = scaleSpeed * Time.fixedDeltaTime * xScaleInput;
        float yScale = scaleSpeed * Time.fixedDeltaTime * yScaleInput;

        if ((transform.localScale.x + xScale) < 0.5) xScale = 0.0f;
        if ((transform.localScale.y + yScale) < 0.5) yScale = 0.0f;

        transform.localScale += new Vector3(xScale, yScale, 0.0f);


    }

    //// Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        xScaleInput = Input.GetAxis("ScaleX");
        yScaleInput = Input.GetAxis("ScaleY");

        //if(transform.position.y < -1)
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        alive = false;
        audioSource.Play();
        // Restart the game
        Invoke("gameOver", 2);   // calls restart function after 2 seconds
    }

    void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameObject.FindGameObjectWithTag("Finish").SetActive(true);
        //GameObject.FindGameObjectWithTag("PlayScreen").SetActive(false);
    }

    public bool isAlive()
    {
        return alive;
    }
}
