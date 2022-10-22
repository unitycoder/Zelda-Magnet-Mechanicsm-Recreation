Shader "Unlit/TestShader"
{
    Properties
    {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
		Tags{ "RenderType"="Transparent" "Queue"="Transparent"}
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float3 ray : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.position = UnityWorldToClipPos(worldPos);
                o.ray = worldPos - _WorldSpaceCameraPos;
                o.screenPos = ComputeScreenPos (o.position);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 screenUv = i.screenPos.xy / i.screenPos.w;
                getProjectedObjectPos(screenUv, i.ray)
            
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
