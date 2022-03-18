using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HUERenderPass : ScriptableRenderPass
{
    public RenderTargetHandle tempTextureHandle;
    private Material material;
    private RenderTargetIdentifier source;

    public HUERenderPass() : base()
    {
        tempTextureHandle.Init("TEMP_HueTexture");
    }

    public void Setup(RenderTargetIdentifier src, Material mat)
    {
        source = src;
        material = mat;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get("HUERenderPass");
        RenderTextureDescriptor camTexDesciptor = renderingData.cameraData.cameraTargetDescriptor;
        cmd.GetTemporaryRT(tempTextureHandle.id, camTexDesciptor, FilterMode.Bilinear);

        Blit(cmd, source, tempTextureHandle.Identifier(), material);
        Blit(cmd, tempTextureHandle.Identifier(), source);

        context.ExecuteCommandBuffer(cmd);

        CommandBufferPool.Release(cmd);
    }
}