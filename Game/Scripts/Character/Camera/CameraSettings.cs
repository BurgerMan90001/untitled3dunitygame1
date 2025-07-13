using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private Transform _fullScreenBlur;
    [SerializeField] private Camera targetCamera;

    [Header("Settings")]
    [SerializeField] private bool _blurScreen;

    [Header("Cull Mask")]
    [SerializeField] private LayerMask cullingMask;

    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;


    private void Awake()
    {
        
    }
    public void OnEnable()
    {
        if (_blurScreen)
        {
            _dialogueData.OnEnterDialogue += ShowBlur;
            _dialogueData.OnExitDialogue += HideBlur;
        }
    }
    public void OnDisable()
    {
        if (_blurScreen) { 
            _dialogueData.OnEnterDialogue -= ShowBlur;
        _dialogueData.OnExitDialogue -= HideBlur;
        }
    }
    private void ShowBlur(string knotName)
    {
        
        targetCamera.cullingMask = ~(1 << cullingMask);
        _fullScreenBlur.gameObject.SetActive(true);
    }
    private void HideBlur()
    {
        _fullScreenBlur.gameObject.SetActive(false);
        targetCamera.cullingMask = -1;
    }
}
