﻿using System.Numerics;

namespace DirectX12GameEngine.Rendering.Materials.CelShading
{
    public interface IMaterialCelShadingLightFunction : IShader
    {
        Vector3 Compute(float lightIn);
    }
}
