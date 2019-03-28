﻿namespace DirectX12GameEngine.Rendering.Materials
{
    public static class MaterialGenerator
    {
        public static Material Generate(MaterialDescriptor descriptor, MaterialGeneratorContext context)
        {
            context.PushMaterialDescriptor(descriptor);

            for (int passIndex = 0; passIndex < context.PassCount; passIndex++)
            {
                MaterialPass materialPass = context.PushPass();

                descriptor.Visit(context);

                materialPass.PipelineState = context.CreateGraphicsPipelineState();
                (materialPass.NativeConstantBufferCpuDescriptorHandle, materialPass.NativeConstantBufferGpuDescriptorHandle) = context.GraphicsDevice.CopyDescriptorsToOneDescriptorHandle(context.ConstantBuffers);
                (materialPass.NativeSamplerCpuDescriptorHandle, materialPass.NativeSamplerGpuDescriptorHandle) = context.GraphicsDevice.CopyDescriptorsToOneDescriptorHandle(context.Samplers);
                (materialPass.NativeTextureCpuDescriptorHandle, materialPass.NativeTextureGpuDescriptorHandle) = context.GraphicsDevice.CopyDescriptorsToOneDescriptorHandle(context.Textures);

                context.PopPass();
            }

            context.PopMaterialDescriptor();

            return context.Material;
        }
    }
}