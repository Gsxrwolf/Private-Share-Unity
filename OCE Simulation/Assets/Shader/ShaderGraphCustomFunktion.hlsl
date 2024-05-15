void GetMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
    Direction = float3(0.5, 0.5, 0);
    Color = float3(1, 1, 0.5);
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
    #if SHADOWS_SCREEN
        half4 clipPos = TransformWorldToHClip(WorldPos);
        half4 shadowCoord = ComputeScreenPos(clipPos);
    #else
        half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    #endif
    Light light = GetMainLight(shadowCoord);
    Direction = light.direction;
    Color = light.color;
    DistanceAtten = light.distanceAttenuation;
    ShadowAtten = light.shadowAttenuation;
#endif
}

void DirectSpecular_float(float3 LightColor, float3 LightDirection, float3 WorldNormal, float3 WorldView, float3 SpecularColor, float Smoothness, out float3 Out)
{
    #if SHADERGRAPH_PREVIEW
        Out = 0;
    #else
        Smoothness = exp2(10 * Smoothness + 1);
        WorldNormal = normalize(WorldNormal);
        WorldView = SafeNormalize(WorldView);
        Out = LightingSpecular(LightColor, LightDirection, WorldNormal, WorldView, float4(SpecularColor, 0), Smoothness);
    #endif
}