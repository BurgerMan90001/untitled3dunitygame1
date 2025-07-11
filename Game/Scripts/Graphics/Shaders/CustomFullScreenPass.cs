using UnityEngine;


using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

class CustomFullScreenPass : CustomPass
{
    public Material fullscreenMaterial;

    protected override void Execute(CustomPassContext ctx)
    {
        if (fullscreenMaterial == null) return;

        // Fullscreen Blit
        CoreUtils.DrawFullScreen(ctx.cmd, fullscreenMaterial, shaderPassId: 0);
    }
}
