// 2025. 11. 07. AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpeakerMusicPlayer : MonoBehaviour
{
    public AudioClip musicClip; // Assign the music clip in the Inspector
    public float volume = 0.5f; // Default volume level

    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the speaker!");
            return;
        }

        // Assign the audio clip and set properties
        audioSource.clip = musicClip;
        audioSource.loop = true; // Play the music clip in a loop
        audioSource.volume = volume;

        // Play the music
        audioSource.Play();
    }
}