using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PacmanController : MonoBehaviour
{
    public float MovementSpeed = 0f;

    public GameObject heart1, heart2, heart3;

    public GameObject pauseMenuUI;
    public GameObject gameoverMenuUI;
    public GameObject gameWinMenuUI;

    public bool isPowerUp = false;

    private Vector3 up = Vector3.zero,
        right = new Vector3(0, 90, 0),
        down = new Vector3(0, 180, 0),
        left = new Vector3(0, 270, 0),
        currentDirection = Vector3.zero;

    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private GameObject PowerUpTxt;
    [SerializeField]
    private Text PowerUpCountdownText;
    [SerializeField]
    private int goals = 242;

    private Vector3 initialPosition = Vector3.zero;

    private new Animation animation;

    private bool isMoving = true;
    private bool isDie = false;
    private bool isWin = false;

    private int score = 0;
    private int health = 3;
    private float timer = 5f;

    public int PacmanHealth { get; private set; }

    public void Reset()
    {
        transform.position = initialPosition;
        animation.Play("Idle");

        currentDirection = down;
        isDie = false;
        isMoving = true;

    }
    // Use this for initialization
    void Start()
    {
        isDie = false;
        isMoving = true;
        QualitySettings.vSyncCount = 0;
        PacmanHealth = health;

        initialPosition = transform.position;
        ScoreText = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();

        animation = GetComponent<Animation>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentDirection = up;
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                currentDirection = right;
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                currentDirection = down;
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentDirection = left;
                isMoving = true;
            }
            // keeps moving? ok...
            // else isMoving = false;

            transform.localEulerAngles = currentDirection;
            if (isMoving)
            {
                animation.Play("Running");
                transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
            }
            //else
            //{
            //    animation.Play("Idle");
            //}
        }
        ScoreText.text = (score * 10).ToString();
        switch (PacmanHealth)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
        }

        if (isPowerUp)
        {
            Debug.Log(timer);
            if (timer < 0f)
            {
                Debug.Log("timer habis");
                PowerUpTxt.SetActive(false);
                timer = 5f;
            }
            else
            {
                PowerUpTxt.SetActive(true);
                timer -= Time.deltaTime;
                PowerUpCountdownText.text = timer.ToString("F");
            }

        }

        if (score >= goals)
        {
            if (!isWin)
            {
                // game win
                gameWinMenuUI.SetActive(true);
                Time.timeScale = 0f;
                isWin = true;
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDie)
        {
            if (collision.gameObject.tag == "Walls")
            {
                animation.Stop();
                animation.Play("Idle");
                isMoving = false;
            }
            if (collision.gameObject.tag == "Waypoint")
            {
                collision.gameObject.SetActive(false);
                score += 1;
            }
        }

        if (collision.gameObject.tag == "Monster")
        {
            if (!isPowerUp)
            {
                isDie = true;
                PacmanHealth -= 1;
                isMoving = false;
                animation.Stop();
                animation.Play("Dying");
                StartCoroutine(DelayDeactivate());
            }

        }
    }
    IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(5f);
        Respawn(PacmanHealth);
    }

    private void Respawn(int health)
    {
        if (health >= 1)
        {
            gameObject.SetActive(true);
            Reset();
        }
        else
        {
            // game over ?
            gameoverMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
