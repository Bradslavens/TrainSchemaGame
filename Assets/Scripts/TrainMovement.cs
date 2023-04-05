using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float speed = 10f; // Adjust this value to set the speed of the train

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move the train forward every frame
        rb.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("signal"))
        {
            Debug.Log("Train has collided with signal!");
            // Stop the train when it collides with an object with the "signal" tag
            rb.velocity = Vector2.zero;
        }
    }
}
