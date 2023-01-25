using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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

    // PowerUp variables
    bool freeze = false;
    bool freezeFuncCalled = false;
    float freezeActiveTime;

    bool hideFuncCalled = false;
    float hideActiveTime;

    public float speedIncreasePerPoint = 0.01f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        freezeActiveTime = 0.0f;
        hideActiveTime = 0.0f;
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        float fwdSpeed = speed;

        if (freeze)
        {
            fwdSpeed = 0.0f;
        }

        Vector3 forwardMove = transform.forward * fwdSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
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

        if (Input.GetButton("Freeze"))
        {
            freezeActiveTime = 3.0f;

            if (!freezeFuncCalled && GameManager.inst.isFreezePowerAvailable())
            {
                freezeFuncCalled = true;
                GameManager.inst.UsePower(PowerType.FREEZE);
            }
        }

        if (freezeActiveTime > 0 && freezeFuncCalled)
        {
            freeze = true;
            freezeActiveTime -= Time.deltaTime;
        }
        else
        {
            freezeFuncCalled = false;
            freeze = false;
        }


        // Invisibility
        if (Input.GetButton("Hide"))
        {
            hideActiveTime = 3.0f;

            if (!hideFuncCalled && GameManager.inst.isInvisiblePowerAvailable())
            {
                hideFuncCalled = true;
                GameManager.inst.UsePower(PowerType.HIDE);
            }
        }

        if (hideActiveTime > 0 && hideFuncCalled)
        {
            this.GetComponent<Collider>().enabled = false;
            hideActiveTime -= Time.deltaTime;
        }
        else
        {
            hideFuncCalled = false;
            this.GetComponent<Collider>().enabled = true;
        }
    }

    public void Die()
    {
        alive = false;
        audioSource.Play();
        // Game Over
        Invoke("gameOver", 2);   // calls restart function after 2 seconds
    }

    void gameOver()
    {
        GameManager.inst.loadGameOverScene();
    }

    public bool isAlive()
    {
        return alive;
    }
}
