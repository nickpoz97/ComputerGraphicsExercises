#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform float weight;

void main()
{
	// linearly interpolate between both textures (80% container, 20% awesomeface)

	//reversed
	vec2 faceReverseCoord = TexCoord;
	faceReverseCoord.x = 1.0 - faceReverseCoord.x;
	FragColor = mix(texture(texture1, TexCoord), texture(texture2, faceReverseCoord), weight);

	//FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
}