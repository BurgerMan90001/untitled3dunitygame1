using UnityEngine;

/// <summary>
/// Handles playing footstep sounds for the player.
/// </summary>
//TODO FIX THIS
public class PlayerFootSteps : MonoBehaviour {
    [Header("Footstep Settings")]
    
    [SerializeField] private AudioClip[] _footstepClips;
    [SerializeField] private float _footstepInterval = 0.5f;

    //[SerializeField] private IsGrounded isGrounded;
    private AudioSource _footstepSource;
    private float _footstepTimer;

    
    /// <summary>
    /// Plays footstep sounds if the player is moving and grounded.
    /// </summary>
    private void Awake()
    {
        _footstepSource = GetComponent<AudioSource>();

    }
    public void FootstepSounds()
    {
        _footstepSource.PlayOneShot(_footstepClips[Random.Range(0, _footstepClips.Length)]);
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
