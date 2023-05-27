#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void sand_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, Texture2D gradient_289980, out float4 Color, out float3 Normal, out float Smoothness, out float4 Emission, out float AmbientOcculusion, out float Metallic, out float4 Specular, out float4 colorOut)
{
	
	float4 _Mapping_264820 = float4(mapping_point(float4(_NOS, 0), float3(0, 0, 0), float3(0, 0, 0), float3(1, 1, 1)), 0);
	float _WaveTexture_264818_fac; float4 _WaveTexture_264818_col; node_tex_wave(_Mapping_264820, 1.9, 3.3, 2.5, -4, 0.7538462, 4.3, 0, 0, 0, 0, _WaveTexture_264818_col, _WaveTexture_264818_fac);
	float _SimpleNoiseTexture_264816_fac; float4 _SimpleNoiseTexture_264816_col; node_simple_noise_texture_full(_Mapping_264820, 0, 150, 15, 0.6, 0, 1, _SimpleNoiseTexture_264816_fac, _SimpleNoiseTexture_264816_col);
	float4 _MixRGB_264826 = mix_blend(0.8083333, _WaveTexture_264818_col, _SimpleNoiseTexture_264816_col);
	float4 _Bump_264828; node_bump(_POS, 1, 0.7833333, 2.999999, _MixRGB_264826, _NTS, _Bump_264828);
	float4 _ColorRamp_289980 = color_ramp(gradient_289980, _Bump_264828);

	Color = _ColorRamp_289980;
	Normal = float3(0.0, 0.0, 0.0);
	Smoothness = 0.0;
	Emission = float4(0.0, 0.0, 0.0, 0.0);
	AmbientOcculusion = 0.0;
	Metallic = 0.0;
	Specular = float4(0.0, 0.0, 0.0, 0.0);
	colorOut = float4(0.0, 0.0, 0.0, 0.0);
}