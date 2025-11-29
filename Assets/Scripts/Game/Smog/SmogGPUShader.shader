Shader "Custom/SmogGPUShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Tint Color", Color) = (1,1,1,1)
        _Size("Sprite Size", Float) = 1.0
        _Alpha("Alpha Multiplier", Float) = 1.0
    }

    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            StructuredBuffer<float4> positions;

            sampler2D _MainTex;
            float4 _Color;
            float _Size;
            float _Alpha;

            struct appdata
            {
                float3 vertex : POSITION;
                float2 uv : TEXCOORD0;
                uint id : SV_InstanceID;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;

                float4 pos = positions[v.id];

                // Escalamos el quad y lo movemos según la posición del compute
                float3 scaled = v.vertex * _Size;

                float3 localPos = pos.xyz + scaled;

                // Transformamos a clip space
                o.vertex = UnityObjectToClipPos(float4(localPos, 1));

                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);

                col *= _Color;
                col.a *= _Alpha;

                return col;
            }
            ENDCG
        }
    }
}
