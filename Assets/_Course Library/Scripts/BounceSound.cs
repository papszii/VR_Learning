// 2025. 11. 07. AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class BallBounceSound : MonoBehaviour
{
    public AudioClip bounceSound; // Assign the bouncing sound effect in the Inspector
    public float volumeMultiplier = 0.1f; // Adjust the volume based on the ball's speed

    private AudioSource audioSource;
    private Rigidbody rb;

    private void Start()
    {
        // Get the AudioSource and Rigidbody components
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        // Ensure the AudioSource is set up properly
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the ball!");
        }

        // Ensure an AudioClip is assigned
        if (bounceSound == null)
        {
            Debug.LogError("Bounce sound effect is not assigned!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Calculate volume based on ball's velocity magnitude
            float volume = Mathf.Clamp(rb.linearVelocity.magnitude * volumeMultiplier, 0.1f, 1.0f);

            // Play the bouncing sound
            audioSource.PlayOneShot(bounceSound, volume);
        }
    }
}