using UnityEngine;
/// <summary>
/// blurs and such
/// </summary>
public class CameraSettings : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private Transform _fullScreenBlur;
    [SerializeField] private Camera targetCamera;

    [Header("Settings")]
    [SerializeField] private bool _blurScreen;

    [Header("Cull Mask")]
    [SerializeField] private LayerMask cullingMask;

    //  [Header("Data")]
    //   [SerializeField] private DialogueData _dialogueData;



    public void OnEnable()
    {
        if (_blurScreen)
        {
            /*
            _dialogueData.Events.OnEnterDialogue += ShowBlur;
            _dialogueData.Events.OnExitDialogue += HideBlur;
            */
        }
    }
    public void OnDisable()
    {
        if (_blurScreen)
        {
            /*
            _dialogueData.Events.OnEnterDialogue -= ShowBlur;
            _dialogueData.Events.OnExitDialogue -= HideBlur;
            */
        }
    }
    private void ShowBlur(string _)
    {

        targetCamera.cullingMask = ~(1 << cullingMask);
        _fullScreenBlur.gameObject.SetActive(true);
    }
    private void HideBlur(GameObject _)
    {
        _fullScreenBlur.gameObject.SetActive(false);
        targetCamera.cullingMask = -1;
    }
}
