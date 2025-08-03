
using UnityEngine.Rendering.HighDefinition;

public interface IColliderTrigger
{

}
public interface IWaterBody : IColliderTrigger
{
    float Density { get; set; }
    float LinearDamping { get; }
    WaterSurface WaterSurface { get; }
}