// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "BFW Shaders/BFWCurtain" {
	Properties {
		_Brightness ("Brightness", Range(0,3)) = 1.0
		_Color ("Color", Color) = (1,1,1,1)
		_Cutoff ("CutOff", Range(0,1)) = 0.5
		_MainTex ("MainTex", 2D) = "white" {}
		_OcclusionTex ("OcclusionTex", 2D) = "white" {}
		_OcclusionStrength ("OcclusionStrength", Range(0,2)) = 1.0

	}
	SubShader {
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard alphatest:_Cutoff

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _OcclusionTex;


		struct Input {
			float2 uv_MainTex;
			float2 uv_OcclusionTex;
		};

		half _Brightness;
		fixed4 _Color;
		half _OcclusionStrength;

		UNITY_INSTANCING_BUFFER_START(Props)

		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float3 ao = lerp(1, tex2D (_OcclusionTex, IN.uv_OcclusionTex),_OcclusionStrength);

			o.Metallic = 0.2;
			o.Smoothness = 0.2;

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb * ao * _Brightness * _Brightness;

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
