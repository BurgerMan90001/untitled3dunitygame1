using UnityEngine;

/// <summary>
/// Handles playing footstep sounds for the player.
/// </summary>

public class PlayerFootSteps : MonoBehaviour {
    [Header("Footstep Settings")]
    
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private float footstepInterval = 0.5f;

    [SerializeField] private IsGrounded isGrounded;
    private AudioSource footstepSource;
    private float footstepTimer;

    
    /// <summary>
    /// Plays footstep sounds if the player is moving and grounded.
    /// </summary>
    private void Awake()
    {
        footstepSource = GetComponent<AudioSource>();

    }
    public void FootstepSounds()
    {
        footstepSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
        /*
        if (isPlayerGrounded.IsGrounded)
        {
            footstepTimer += Time.fixedDeltaTime;
            if (footstepTimer >= footstepInterval)
            {
                footstepTimer = 0f;
                if (footstepClips.Length > 0 && footstepSource != null)
                {
                    footstepSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
                }
            }
        }
        else
        {
            footstepTimer = 0f;
        }
        */

    }
    
}
