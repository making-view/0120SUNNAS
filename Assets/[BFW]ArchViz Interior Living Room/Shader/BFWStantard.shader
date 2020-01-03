// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "BFW Shaders/BFWStantard" {
	Properties {
		_Brightness ("Brightness", Range(0,3)) = 1.0
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("MainTex", 2D) = "white" {}
		_MetallicGlossMap ("MetallicGlossMap", 2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Glossiness ("Glossiness", Range(0,1)) = 0.5
		_BumpMap ("BumpMap1", 2D) = "bump" {}
		_BumpScale ("Bump1Scale", float) = 1.0
		_BumpMap2 ("BumpMap2", 2D) = "bump" {}
		_BumpScale2 ("Bump2Scale", float) = 1.0

		_EmissionValue ("EmissionValue", float) = 0.0
		_EmissionColor ("EmissionColor", Color) = (1,1,1,1)
		_EmissionMap ("EmissionMap", 2D) = "white" {}

		_OcclusionMap ("OcclusionMap", 2D) = "white" {}
		_OcclusionStrength ("OcclusionStrength", Range(0,2)) = 1.0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _BumpMap;
		sampler2D _BumpMap2;
		sampler2D _EmissionMap;
		sampler2D _OcclusionMap;


		struct Input {
			float2 uv_MainTex;
			float2 uv_MetallicGlossMap;
			float2 uv_BumpMap;
			float2 uv_BumpMap2;
			float2 uv_EmissionMap;
			float2 uv_OcclusionMap;

		};

		half _Brightness;
		fixed4 _Color;
		half _Metallic;
		half _Glossiness;
		half _BumpScale;
		half _BumpScale2;
		fixed4 _EmissionColor;
		half _EmissionValue;
		half _OcclusionStrength;


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float3 ao = lerp(1, tex2D (_OcclusionMap, IN.uv_OcclusionMap),_OcclusionStrength);
			o.Occlusion = ao;

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb * _Brightness * _Brightness;

			fixed4 m = tex2D (_MetallicGlossMap, IN.uv_MetallicGlossMap);
			o.Metallic = _Metallic * m.rgb;
			o.Smoothness = _Glossiness * m.a;

			float3 Norm1 = UnpackNormal (tex2D (_BumpMap , IN.uv_BumpMap));
			float3 Norm2 = UnpackNormal (tex2D (_BumpMap2 , IN.uv_BumpMap2));

			o.Normal = float3(Norm1.xy * _BumpScale + Norm2.xy * _BumpScale2,(Norm1.z+Norm2.z)*0.5);

			fixed3 e = tex2D (_EmissionMap, IN.uv_EmissionMap).rgb * saturate(_EmissionColor) * _EmissionValue * _EmissionValue;
			o.Emission = e;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
