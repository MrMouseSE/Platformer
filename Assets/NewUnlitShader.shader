Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _FresnelColor ("Fren Col", color) = (1,1,1,1)
        _FresnelPower ("Fren Power", range(0,10)) = 2
        _MainColor ("Main Col", color) = (1,1,1,1)
        _SpecularPower ("Spec Pow", range(0.1,50)) = 4
        _SpecularCorrection ("Spec Correction", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed fresnel : COLOR0;
                fixed3 normal : NORMAL;
                float specular : COLOR1;
            };

            float4 _FresnelColor,_MainColor;
            float _SpecularPower, _FresnelPower, _SpecularCorrection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 worldNormal = mul(unity_ObjectToWorld,v.normal);
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
                o.fresnel = pow(dot(worldNormal,viewDir), _FresnelPower);
                o.normal = worldNormal;
                o.specular = pow(max(0,dot(v.normal, _WorldSpaceLightPos0)), _SpecularPower) - _SpecularCorrection;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normalDir = normalize(i.normal);
                float spec = pow(max(0,dot(normalDir, _WorldSpaceLightPos0)), _SpecularPower);
                fixed4 col = saturate(_MainColor + saturate(spec));
                col = lerp(_FresnelColor,col,saturate(i.fresnel));
                return col;
            }
            ENDCG
        }
    }
}