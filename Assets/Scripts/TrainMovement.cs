using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float speed = 10f; // Adjust this value to set the speed of the train

    private Rigidbody2D rb;
    private UIController uiController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get a reference to the UIController script
        uiController = FindObjectOfType<UIController>();

        // Hide the UI panel on start (in case it's not already hidden)
        uiController.HideUI();
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
            // Stop the train when it collides with an object with the "signal" tag
            rb.velocity = Vector2.zero;

            // Show the UI panel when the train collides with a signal
            uiController.ShowUI();
        }
    }
}
