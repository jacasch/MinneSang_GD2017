Shader "Custom/AuraEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Intensity("WobbleIntensity", Range(0.001, 20)) = 0.001
		_Tint("Tint", Color) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

		GrabPass{ "_GrabTextureAura" }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 grabPos: TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _GrabTextureAura;
			float4 _MainTex_ST;
			float4 _Tint;
			float _Intensity;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				// use ComputeGrabScreenPos function from UnityCG.cginc
				// to get the correct texture coordinate
				o.grabPos = ComputeGrabScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 texCol = tex2D(_MainTex, i.uv);
				//texCol = floor(texCol * 4) / 2;

				//calculate offset
				float4 offset = _Intensity * texCol.r;
				offset.x *= sin(_Time * 50 + i.uv.x * 10);
				offset.y *= sin(_Time * 67.1238 + i.uv.y * 8.6128);
				i.grabPos += offset;
				
				//Sample the grabed texture
				fixed4 col = tex2Dproj(_GrabTextureAura, i.grabPos);

				//col -= 0.02;
				//col.rgb = (1,1,1) - col.rgb;
				col.a = texCol.a;
				col += _Tint * texCol.r;
				return col;
			}
			ENDCG
		}
	}
}
