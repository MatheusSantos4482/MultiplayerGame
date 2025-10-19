Shader "Custom/CellShadingMultiLight"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color Tint", Color) = (1,1,1,1)
        _ShadeLevels ("Shade Levels", Range(2,6)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf CellShading fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _ShadeLevels;

        struct Input
        {
            float2 uv_MainTex;
        };

        // nossa função de iluminação personalizada
        inline half4 LightingCellShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        {
            // intensidade da luz difusa
            float NdotL = saturate(dot(s.Normal, lightDir));

            // quantização
            float stepVal = floor(NdotL * _ShadeLevels) / (_ShadeLevels - 1);

            half3 c;
            c = s.Albedo * _LightColor0.rgb * stepVal * atten;

            return half4(c, s.Alpha);
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = tex.rgb;
            o.Alpha = tex.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
