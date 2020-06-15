Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_PixelOffset("Pixel Offset", Range(-1, 1)) = 0.5
		_ColorPalette("Color Palette", 2D) = "white" {}
		_PaletteW("Palette Width", Int) = -1
		_PaletteH("Palette Height", Int) = -1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float _PixelOffset;
			float _OrthoSize;
			
			sampler2D _ColorPalette;
			int _PaletteW;
			int _PaletteH;
			
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
				
				float pixelsY = _OrthoSize * 2 * 32;
				float pixelsX = (pixelsY * _ScreenParams.x) / _ScreenParams.y;
				
				float2 gridSize = float2(pixelsX, pixelsY);
				
				float2 cameraSize = _ScreenParams.xy;
				float2 pixelSize = cameraSize / gridSize;
				
				float2 uv = (i.uv * cameraSize) / pixelSize;
				uv = (float2(int(uv.x), int(uv.y)) * pixelSize + pixelSize * _PixelOffset)/cameraSize;
				
                fixed4 col =  tex2D(_MainTex, uv);
				
				
				if(!(_PaletteH < 1 || _PaletteW < 1))
				{
					fixed4 paletteMatch = float4(1,1,1,1);
					float matchDistance = 10;
					
					for(uint x = 0; x < _PaletteW; ++x)
					{
						for(uint y = 0; y < _PaletteH; ++y)
						{
							float2 uv = float2((x + 0.1) / _PaletteW, (y + 0.1) / _PaletteH);
							
							fixed4 paletteSample = tex2D(_ColorPalette, uv);
								
							float currentDistance = distance(col.rgb, paletteSample.rgb);
							
							if(currentDistance < matchDistance)
							{
								matchDistance = currentDistance;
								paletteMatch.rgb = paletteSample.rgb;
							}
						}
					}
					
					col.rgb = paletteMatch.rgb;
				}
				
				return col;
            }
            ENDCG
        }
    }
}
