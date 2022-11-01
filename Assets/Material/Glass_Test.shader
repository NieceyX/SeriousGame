Shader "Unlit/Glass_Test"
{
    Properties
    {
       _DiffuseColor("Diffuse Color", Color) = (1,1,1,1)
        _Opacity("Opacity",Range(0,1)) = 0
        _AddColor("Add Color",Color) = (1,1,1,1)
        _Distort("Distort", Range(0,1)) = 0
        _Power("Power" ,Float) = 1.0
        _Scale("Scale" ,Float) = 1.0
            _ThicknessMap("ThicknessMap", 2D) = "white"{}
        _CubeMap("Cube Map", Cube) = "white"{}
        _EnvRotate("Env Rotate", Range(0,360)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
 
                Tags{"LightMode" = "ForwardAdd"}
                Blend One One
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"

            struct appdata
            {
                   float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                    float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 normal_world : TEXCOORD1;
                float3 pos_world : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightColor0;
            float _Distort;
            float _Power;
            float _Scale;
            sampler2D _ThicknessMap;
            samplerCUBE _CubeMap;
            float4 _CubeMap_HDR;
            float _EnvRotate;
            float4 _DiffuseColor;
            float _AddColor;
            float _Opacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.normal_world = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
                o.pos_world = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }


                half4 frag(v2f i) : SV_Target
            {

                float3 normal_dir = normalize(i.normal_world);
                float3 view_dir = normalize(_WorldSpaceCameraPos.xyz - i.pos_world);
                float3 light_dir = normalize(_WorldSpaceLightPos0.xyz - i.pos_world);

                float NdotL = max(0.0, dot(normal_dir, light_dir));
                float3 back_dir = -normalize(light_dir + normal_dir * _Distort);
                float VdotB = dot(view_dir, -light_dir);
                float backlight_term = max(0.0, pow(VdotB, _Power)) * _Scale;
                float thickness = tex2D(_ThicknessMap, i.uv).r;
                float3 diffuse_term = backlight_term * _LightColor0.xyz * thickness;
                return float4(diffuse_term,1.0);
            }
            ENDCG
        }
    }
}
