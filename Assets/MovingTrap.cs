using UnityEngine;
using System.Collections.Generic;

public class MovingTrap : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public float leftLimit = -4f;
    public float rightLimit = 4f;

    private bool movingRight = true;

    void Update()
    {
        // Move left and right
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
                movingRight = true;
        }
    }
}
