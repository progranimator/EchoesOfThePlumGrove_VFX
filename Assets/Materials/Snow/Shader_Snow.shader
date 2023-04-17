Shader "Custom/Shader_Snow" {
    Properties {
        _SnowColor ("Snow Color", Color) = (1,1,1,1)
        _SnowFlakeSpeed ("Snow Flake Speed", Range(0.1, 5.0)) = 1.0
        _SnowFallSpeed ("Snow Fall Speed", Range(0.1, 5.0)) = 1.0
        _WindSpeed ("Wind Speed", Range(0.1, 10.0)) = 1.0
        _WindDirection ("Wind Direction", Vector) = (0,0,1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _SnowColor;
            float _SnowFlakeSpeed;
            float _SnowFallSpeed;
            float _WindSpeed;
            float3 _WindDirection;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float4 snow = tex2D(_MainTex, i.uv);
                float2 windOffset = float2(_Time.y * _WindSpeed, 0);
                float2 snowOffset = i.uv + windOffset + (_WindDirection.xy * snow.a * _SnowFlakeSpeed);
                float2 snowOffset2 = i.uv + windOffset + (_WindDirection.xy * (snow.a + 0.1) * _SnowFallSpeed);
                float4 finalSnow = tex2D(_MainTex, snowOffset) + tex2D(_MainTex, snowOffset2);
                return finalSnow * _SnowColor * snow.a;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
