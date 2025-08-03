using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Lake : MonoBehaviour, IWaterBody
{
    [Header("Settings")]
    [field: SerializeField] public float Density { get; set; }
    [field: SerializeField] public float LinearDamping { get; private set; }
    public WaterSurface WaterSurface { get; private set; }


    private void Awake()
    {
        WaterSurface = GetComponent<WaterSurface>();
    }
}
