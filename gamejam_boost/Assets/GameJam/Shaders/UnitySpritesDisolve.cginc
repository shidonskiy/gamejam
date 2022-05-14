// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

#ifndef UNITY_SPRITES_INCLUDED
#define UNITY_SPRITES_INCLUDED

#include "UnityCG.cginc"

#ifdef UNITY_INSTANCING_ENABLED

    UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
        // SpriteRenderer.Color while Non-Batched/Instanced.
        UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
        // this could be smaller but that's how bit each entry is regardless of type
        UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
    UNITY_INSTANCING_BUFFER_END(PerDrawSprite)

    #define _RendererColor  UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
    #define _Flip           UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)

#endif // instancing

CBUFFER_START(UnityPerDrawSprite)
#ifndef UNITY_INSTANCING_ENABLED
    fixed4 _RendererColor;
    fixed2 _Flip;
#endif
    float _EnableExternalAlpha;
CBUFFER_END

// Material Color.
fixed4 _Color;

float _DissolveRadius;
float4 _DissolvePosition;
float _DissolveLineWidth;
fixed4 _DissolveLineColor;
float _DissolveAmount;
float _DissolveInverse;
float _DissolveEnabled;
sampler2D _DissolveNoiseMap;
fixed4 _DissolveNoiseMap_ST;

struct appdata_t
{
    float4 vertex   : POSITION;
    float4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float4 vertex   : SV_POSITION;
    fixed4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
    float4 worldPos : TEXCOORD1;
    UNITY_VERTEX_OUTPUT_STEREO
};

inline float4 UnityFlipSprite(in float3 pos, in fixed2 flip)
{
    return float4(pos.xy * flip, pos.z, 1.0);
}

v2f SpriteVert(appdata_t IN)
{
    v2f OUT;

    UNITY_SETUP_INSTANCE_ID (IN);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

    OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
    OUT.vertex = UnityObjectToClipPos(OUT.vertex);
    OUT.worldPos = mul (unity_ObjectToWorld, IN.vertex);;
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color * _RendererColor;

    #ifdef PIXELSNAP_ON
    OUT.vertex = UnityPixelSnap (OUT.vertex);
    #endif

    return OUT;
}

sampler2D _MainTex;
sampler2D _AlphaTex;

fixed4 SampleSpriteTexture (float2 uv)
{
    fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
    fixed4 alpha = tex2D (_AlphaTex, uv);
    color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif

    return color;
}

fixed4 SpriteFrag(v2f IN) : SV_Target
{
    //Dissolve
    fixed4 noise = 1 - tex2D(_DissolveNoiseMap, (IN.worldPos.xy + _Time.y) * 0.5);
    float dist = distance(_DissolvePosition.xy, IN.worldPos.xy);
    float sphere = (dist + noise.r * _DissolveAmount) / _DissolveRadius;
    float clipValue = lerp(sphere - 1, 1 - sphere, 1 - _DissolveInverse);

    //clipValue = lerp(dist - _DissolveRadius, _DissolveRadius - dist, 1 - _DissolveInverse);
    clipValue = lerp(1, clipValue, _DissolveEnabled);
    
    clip(clipValue);
    
    fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
    c.rgb *= c.a;
    
    // float border = smoothstep(_DissolveAmount, _DissolveAmount + _DissolveLineWidth, sphere);
    // border = lerp(border, 1 - border, 1 - _DissolveInverse);
    // float3 disLine = 1 - border;
    // disLine *= _DissolveLineColor.rgb;
    //
    // c.rgb = border * c.rgb;
    // c.rgb += disLine;
    
    return c;
}

#endif // UNITY_SPRITES_INCLUDED
