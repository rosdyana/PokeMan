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
        var isMoving = true;

        if (Input.GetKey(KeyCode.UpArrow)) currentDirection = up;
        else if (Input.GetKey(KeyCode.RightArrow)) currentDirection = right;
        else if (Input.GetKey(KeyCode.DownArrow)) currentDirection = down;
        else if (Input.GetKey(KeyCode.LeftArrow)) currentDirection = left;
        else isMoving = false;

        transform.localEulerAngles = currentDirection;
        if (isMoving)
        {
            animation.Play("Running");
            transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
        }
        else
        {
            animation.Play("Idle");
        }

    }
}
