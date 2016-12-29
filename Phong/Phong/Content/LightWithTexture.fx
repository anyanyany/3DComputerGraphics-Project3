float3 CameraPosition;

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 LightPosition;

texture ModelTexture;
sampler2D textureSampler = sampler_state {
	Texture = (ModelTexture);
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float3 Normal : NORMAL0;
	float2 UV : TEXCOORD0;
};
struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float3 Normal : TEXCOORD0;
	float4 WorldPosition : TEXCOORD1;
	float2 UV : TEXCOORD2;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;
	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	output.WorldPosition = worldPosition;
	output.Normal = mul(input.Normal, (float3x3)World);
	output.UV = input.UV;
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 phonglLight = 0;

	float3 pd1 = float3(0.16, 0.19, 0.14);
	float3 pd2 = float3(0.03, 0.04, 0.08);
	float3 pd3 = float3(0.06, 0.13, 0.24);

	float3 ps1 = float3(1.27, 0.69, 0.75);
	float3 ps2 = float3(0.62, 0.19, 1.48);
	float3 ps3 = float3(0.00, 0.80, 3.10);
	float3 ps4 = float3(0.83, 0.02, 0.00);

	float3 c1 = float3(-1.01, -1.01, 1.00);
	float3 c2 = float3(-1.01, -1.01, 1.00);
	float3 c3 = float3(-0.95, -0.95, 0.97);
	float3 c4 = float3(-0.49, -0.49, 0.38);

	float m1 = 346;
	float m2 = 231;
	float m3 = 27;
	float m4 = 502;

	float3 L = normalize(LightPosition - input.WorldPosition.xyz);
	float3 N = normalize(input.Normal);
	float3 V = normalize(CameraPosition - input.WorldPosition.xyz);
	float3 R = -reflect(L, N);

	float3 one = float3(0, 1, 0);
	float3 T = cross(one, N);
	float3 B = cross(N, T);

	T = normalize(T);
	B = normalize(B);

	float3 Lprim = float3(dot(T, L), dot(B, L), dot(N, L));
	float3 Vprim = float3(dot(T, V), dot(B, V), dot(N, V));

	float diffuseFactor = saturate(Lprim.z); 

	float3 color1 = pd1 * diffuseFactor + ps1 * pow(saturate(c1.x*Lprim.x*Vprim.x + c1.y*Lprim.y*Vprim.y + c1.z*Lprim.z*Vprim.z), m1);
	float3 color2 = pd2 * diffuseFactor + ps2 * pow(saturate(c2.x*Lprim.x*Vprim.x + c2.y*Lprim.y*Vprim.y + c2.z*Lprim.z*Vprim.z), m2);
	float3 color3 = pd3 * diffuseFactor + ps3 * pow(saturate(c3.x*Lprim.x*Vprim.x + c3.y*Lprim.y*Vprim.y + c3.z*Lprim.z*Vprim.z), m3) + ps4 * pow(saturate(c4.x*Lprim.x*Vprim.x + c4.y*Lprim.y*Vprim.y + c4.z*Lprim.z*Vprim.z), m4);


	float4 textureColor = tex2D(textureSampler, input.UV);
	phonglLight += (color1*textureColor.x + color2*textureColor.y + color3*textureColor.z);

	float3 light = saturate(phonglLight);
	return float4(light, 1);
}

technique Light
{
	pass Pass1
	{
		VertexShader = compile vs_4_0 VertexShaderFunction();
		PixelShader = compile ps_4_0 PixelShaderFunction();
	}
}