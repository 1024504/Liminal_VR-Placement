Shader "SeafloorPBR" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	    [Normal] _NormalTex("Normal Map", 2D) = "bump" {}
	    _GeneralColour ("General Colour", Color) = (0,0.5,1)
	    _GeneralColourStrength ("General Colour Strength", Range(0,1)) = 0.5
	    _CausticColour ("Caustic Colour", Color) = (1,1,1)
	    _CausticStrength ("Caustic Strength", Range(0,1)) = 1
	    _CausticSize ("Caustic Size", Float) = 2
	    _CausticSpeed ("Caustic Speed", Float) = 0
}
	SubShader {
		Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

        #include "Random.cginc"

		float _CausticSize;
		float _CausticSpeed;
		float _CausticStrength;
		sampler2D _MainTex;
		float3 _GeneralColour;
		float _GeneralColourStrength;
		float3 _CausticColour;
		sampler2D _NormalTex;

		struct Input {
			float3 worldPos;
		    float2 uv_MainTex;
		};

		float voronoiNoise(float2 value){
		    float2 baseCell = floor(value);

		    float minDistToCell = 10;
		    [unroll]
		    for(int x=-1; x<=1; x++){
		        [unroll]
		        for(int y=-1; y<=1; y++){
		            float2 cell = baseCell + float2(x, y);
		            float2 cellPosition = cell + voronoi(cell, _Time[1]*_CausticSpeed);
		            float2 toCell = cellPosition - value;
		            float distToCell = length(toCell);
		            if(distToCell < minDistToCell){
		                minDistToCell = distToCell;
		            }
		        }
		    }
		    return minDistToCell;
		}

		void surf (Input i, inout SurfaceOutputStandard o) {
			float2 value = i.worldPos.xz / _CausticSize;
			float noise = clamp(pow(voronoiNoise(value), 6), 0, 1) * _CausticStrength;
		    float3 colour = lerp(lerp(tex2D(_MainTex, i.uv_MainTex), _GeneralColour, _GeneralColourStrength), _CausticColour, noise);
		    
			o.Albedo = colour;
		    o.Normal = tex2D(_NormalTex, i.uv_MainTex).rgb;
		    o.Metallic = 0;
		    o.Smoothness = 0.5;
		}
		ENDCG
	}
	FallBack "Standard"
}
