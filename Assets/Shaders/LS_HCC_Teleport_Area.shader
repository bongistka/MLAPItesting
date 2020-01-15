Shader "Lightshape/HCC/Teleport/TeleportArea" {
    Properties {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Thickness("Outline Thickness", float) = 1

        [KeywordEnum(Less, Greater, LEqual, GEqual, Equal, NotEqaul, Always)] _ZTest ("ZTest", Int) = 2
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    half4 _Color;
    sampler2D _MainTex;
    float4 _MainTex_ST;
    float _Thickness;

    struct v2g {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
        float3 viewT : TANGENT;
        float3 normals : NORMAL;
    };
             
    struct g2f {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
        float3 viewT : TANGENT;
        float3 normals : NORMAL;
    };

    v2g vert(appdata_base v) {
        v2g OUT;


        OUT.pos = UnityObjectToClipPos(v.vertex);

        OUT.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

        OUT.normals = v.normal;
        OUT.viewT = ObjSpaceViewDir(v.vertex);
        return OUT;
    }

    ENDCG

    SubShader {
        Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZTest [_ZTest]

        Pass {
            Stencil {
                Ref 1
                Comp always
                Pass replace
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
                         
            half4 frag(g2f IN) : COLOR {
                fixed4 col = tex2D(_MainTex, IN.uv) * _Color;
                return col;
            }
            ENDCG
        }

        Pass {
            Stencil {
                Ref 0
                Comp equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag    
             
            void geom2(v2g start, v2g end, inout TriangleStream<g2f> triStream) {
                float4 parallel = end.pos-start.pos;
                parallel = normalize(parallel) * 0.01 * _Thickness; 

                float4 perpendicular = float4(parallel.y,-parallel.x, 0, 0);
                perpendicular = normalize(perpendicular) * 0.1f * _Thickness;

                float4 v1 = start.pos - parallel;
                float4 v2 = end.pos + parallel;

                g2f OUT;

                OUT.pos = v1-perpendicular;
                OUT.uv = start.uv;
                OUT.viewT = start.viewT;
                OUT.normals = start.normals;
                triStream.Append(OUT);
                 
                OUT.pos = v1+perpendicular;
                triStream.Append(OUT);
                 
                OUT.pos = v2-perpendicular;
                OUT.uv = end.uv;
                OUT.viewT = end.viewT;
                OUT.normals = end.normals;
                triStream.Append(OUT);
                 
                OUT.pos = v2+perpendicular;
                OUT.uv = end.uv;
                OUT.viewT = end.viewT;
                OUT.normals = end.normals;
                triStream.Append(OUT);
             }
             
            [maxvertexcount(12)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) {
                geom2(IN[0],IN[1],triStream);
                geom2(IN[1],IN[2],triStream);
                geom2(IN[2],IN[0],triStream);
            }
             
            half4 frag(g2f IN) : COLOR {
                _Color.a = 1;
                return _Color;
            }
            ENDCG 
         }
     }
     FallBack "Diffuse"
 }