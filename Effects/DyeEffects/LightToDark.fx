sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float wave = 1 - sin(coords.x + uTime) * 2;
    color.rgb = color.rgb / wave;
    return color * sampleColor;
}

technique Technique1
{
    pass LightToDarkPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
/*
* A few mental notes:
* 1) float2s are like Vector2s
* 2) float3s are Vector3s
* 3) float4s are Vector4s
*/