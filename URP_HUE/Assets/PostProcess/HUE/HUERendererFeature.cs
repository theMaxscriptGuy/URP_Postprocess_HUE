using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HUERendererFeature : ScriptableRendererFeature
{
    public Material RenderMaterial;
    private HUERenderPass huePass;

    public override void Create()
    {
        huePass = new HUERenderPass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        huePass.Setup(renderer.cameraColorTarget, RenderMaterial);
        huePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        renderer.EnqueuePass(huePass);
    }
}