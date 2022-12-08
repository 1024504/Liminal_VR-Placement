Shader "WaterSurfacePBR"
{
	Properties
    {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	    [Normal] _NormalTex("Normal Map", 2D) = "bump" {}
	    _WaterColour ("Water Colour", Color) = (0,0.5,1)
	    _SkyColour ("Sky Colour", Color) = (1,1,1)
	    _SkyColourStrength ("Sky Colour Strength", Range(0,1)) = 1
	    _NormalSpeed1 ("Normal Speed 1", Float) = 2
	    _NormalSpeed2 ("Normal Speed 2", Float) = 2
	    _FresnelPower ("Fresnel Power", Range(0,1)) = 0.5
    }
    
	SubShader
    {
		Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalTex;
		float3 _WaterColour;
		float3 _SkyColour;
		float _SkyColourStrength;
		float _NormalSpeed1;
		float _NormalSpeed2;
		float _FresnelPower;

		struct Input
		{
			float3 worldPos;
		    float2 uv_MainTex;
		    float3 viewDir;
		};

		float FresnelEffect(float3 Normal, float3 ViewDir, float Power)
		{
		    return pow(1.0 - saturate(dot(normalize(Normal), normalize(ViewDir))), Power);
		}

		void surf (Input i, inout SurfaceOutputStandard o)
		{
            const float3 normalScroll = lerp(UnpackNormal(tex2D(_NormalTex, i.uv_MainTex+_NormalSpeed1*_Time[1])), tex2D(_NormalTex, i.uv_MainTex+_NormalSpeed2*_Time[1]).rgb, 0.3);

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
