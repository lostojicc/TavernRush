Shader "CardboardXRPlugin/Reticle"
{
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 1)
		_InnerDiameter ("InnerDiameter", Range(0, 10.0)) = 1.5
		_OuterDiameter ("OuterDiameter", Range(0.00872665, 10.0)) = 2.0
		_DistanceInMeters ("DistanceInMeters", Range(0.0, 100.0)) = 2.0
		_Fill ("Fill", Range(0, 1)) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue"="Overlay"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha, OneMinusDstAlpha One
			Cull Back
			Lighting Off
			ZWrite Off
			ZTest Always
			Fog { Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float4 _Color;
			uniform float _InnerDiameter;
			uniform float _OuterDiameter;
			uniform float _DistanceInMeters;
			uniform float _Fill;

			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			struct fragmentInput
			{
				float4 position : SV_POSITION;
				float2 screenPos : TEXCOORD0;
			};

			fragmentInput vert(vertexInput i)
			{
				float scale = lerp(_OuterDiameter, _InnerDiameter, i.vertex.z);

				float3 vert_out = float3(
					i.vertex.x * scale,
					i.vertex.y * scale,
					_DistanceInMeters
				);

				fragmentInput o;
				o.position = UnityObjectToClipPos(vert_out);
				o.screenPos = vert_out.xy;
				return o;
			}

			fixed4 frag(fragmentInput i) : SV_Target
			{
				// No fill ? draw full reticle (dot / ring)
				if (_Fill <= 0.001)
					return _Color;

				float angle = atan2(i.screenPos.y, i.screenPos.x);
				angle = (angle + UNITY_PI) / (2.0 * UNITY_PI);

				if (angle > _Fill)
					discard;

				return _Color;
			}
			ENDCG
		}
	}
}
