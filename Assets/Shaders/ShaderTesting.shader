Shader ".MyShaders/Fixed Unlit" 
{
	SubShader
	{
		Pass
		{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		struct appdata
		{
			float4 vertex : POSITION
		};

		struct v2f
		{
			float4 pos : SV_POSITION
		};

		v2f vert(appdata IN) 
		{
			v2f OUT;
			OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);
			return OUT;
		}

		fixed4 frag(v2f IN) : COLOR
		{
			return fixed4(1, 0, 0, 1);
		}
		ENDCG
		}
	}
}