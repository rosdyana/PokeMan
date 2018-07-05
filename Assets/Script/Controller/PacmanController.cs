using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour
{

    public float MovementSpeed = 0f;

    private Vector3 up = Vector3.zero,
        right = new Vector3(0, 90, 0),
        down = new Vector3(0, 180, 0),
        left = new Vector3(0, 270, 0),
        currentDirection = Vector3.zero;

    private Vector3 initialPosition = Vector3.zero;

    private new Animation animation;

    private bool isMoving = true;

    public void Reset()
    {
        transform.position = initialPosition;
        animation.Play("Idle");

        currentDirection = down;
    }
    // Use this for initialization
    void Start()
    {
        QualitySettings.vSyncCount = 0;

        initialPosition = transform.position;
        animation = GetComponent<Animation>();
        Reset();
    }

    // Update is called once per frame
    void Update()
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

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Walls")
        {
            animation.Stop();
            animation.Play("Idle");
            isMoving = false;
        }
        if (collision.gameObject.tag == "Waypoint")
        {
            Debug.Log("GOTCHA");
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Monster")
        {
            isMoving = false;
            animation.Stop();
            animation.Play("Dying");
            StartCoroutine(DelayDeactivate());
        }
    }
    IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
