float3 CameraPosition;

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 Ka;
float3 Kd;
float3 Ks;

float3 LightPosition;
float Ia;
float Id;
float Is;
float Shininess;

float Attenuation;
float Falloff;

float3 pentagonPoints[12];
float3 hexagonPoints[20];

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float3 Normal : TEXCOORD0;
	float4 WorldPosition : TEXCOORD1;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;
	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	output.WorldPosition = worldPosition;
	output.Normal = mul(input.Normal, (float3x3)World);
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float minDistPentagon = 1000;
	float minDistHexagon = 1000;
	float minDistFirst = 1000;
	float minDistSecond = 1000;
	float dst;

	for (int i = 0; i < 12; i++)
	{
		dst = abs(distance(pentagonPoints[i], input.WorldPosition.xyz));
		if (dst < minDistPentagon)
			minDistPentagon = dst;
		if (dst < minDistFirst)
		{
			minDistSecond = minDistFirst;
			minDistFirst = dst;
		}
		else if (dst < minDistSecond)
			minDistSecond = dst;
	}

	for (int i = 0; i < 20; i++)
	{
		dst = abs(distance(hexagonPoints[i], input.WorldPosition.xyz));
		if (dst < minDistHexagon)
			minDistHexagon = dst;
		if (dst < minDistFirst)
		{
			minDistSecond = minDistFirst;
			minDistFirst = dst;
		}
		else if (dst < minDistSecond)
			minDistSecond = dst;
	}

	float3 Ambient = float3(0, 0, 0);
	if (minDistPentagon < minDistHexagon)
		Ambient = float3(0, 0, 1);
	else
	Ambient = float3(1, 1, 1);

	float diff = minDistFirst - minDistSecond;
	if (diff < 0)
		diff = -diff;
	float interpolation = smoothstep(0, 0.03, diff);
	Ambient *= interpolation;

	float3 phonglLight = 0;
	Ia = Is + Id;
	Ia = clamp(Ia / 2, 0, 1);
	phonglLight += Ambient*Ia;

	float3 L = normalize(LightPosition - input.WorldPosition.xyz);
	float3 N = normalize(input.Normal);
	float3 V = normalize(CameraPosition - input.WorldPosition.xyz);
	float3 R = -reflect(L, N);

	float diffuseFactor = saturate(dot(N, L));
	float3 Diffuse = diffuseFactor * Kd * Id;

	float specularFactor = pow(saturate(dot(R, V)), Shininess);
	float3 Specular = specularFactor * Ks * Is;

	float dist = distance(LightPosition, input.WorldPosition.xyz);
	float att = 1 - pow(clamp(dist / Attenuation, 0, 1), Falloff);
	phonglLight += (Diffuse + Specular)*att;


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