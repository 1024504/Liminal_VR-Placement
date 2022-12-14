Shader "WaterSurfacePBR"
{
	Properties
    {
	    [Normal] _NormalTex("Normal Map", 2D) = "bump" {}
	    _WaterColour ("Water Colour", Color) = (0,0.5,1)
	    _SkyColour ("Sky Colour", Color) = (1,1,1)
	    _SkyColourStrength ("Sky Colour Strength", Range(0,1)) = 1
	    [ShowAsVector2] _NormalSpeed1 ("Normal Speed 1", Vector) = (0,0,0,0)
	    [ShowAsVector2] _NormalSpeed2 ("Normal Speed 2", Vector) = (0,0,0,0)
	    _FresnelPower ("Fresnel Power", Range(0,1)) = 0.5
    }
    
	SubShader
    {
		Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _NormalTex;
		float3 _WaterColour;
		float3 _SkyColour;
		float _SkyColourStrength;
		float2 _NormalSpeed1;
		float2 _NormalSpeed2;
		float _FresnelPower;

		struct Input
		{
			float3 worldPos;
		    float2 uv_MainTex;
		    float2 uv_NormalTex;
		    float3 viewDir;
		};

		float FresnelEffect(float3 Normal, float3 ViewDir, float Power)
		{
		    return pow(1.0 - saturate(dot(normalize(Normal), normalize(-ViewDir))), Power);
		}

		void surf (Input i, inout SurfaceOutputStandard o)
		{
            float3 normalScroll = lerp(UnpackNormal(tex2D(_NormalTex, i.uv_NormalTex+_NormalSpeed1.xy*_Time[1])), UnpackNormal(tex2D(_NormalTex, i.uv_NormalTex+_NormalSpeed2.xy*_Time[1])).rgb, 0.5);
            normalScroll.b *= -1;
		    
		    const float3 albedo = lerp(_WaterColour, _SkyColour,(1-FresnelEffect(normalScroll, i.viewDir, _FresnelPower))*_SkyColourStrength);
		    
			o.Albedo = albedo;
		    o.Normal = normalScroll;
		    o.Metallic = 0;
		    o.Smoothness = 0.5;
		}
		ENDCG
	}
	FallBack "Standard"
}
