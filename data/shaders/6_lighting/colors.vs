#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out vec3 Normal;
out vec3 FragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat3 normalTransform;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0);

	Normal = normalTransform * aNormal;
	// la luce verr� calcolata con la posizione dei vertici nello spazio mondo (perch� la luce � nello spazio mondo)
	FragPos = vec3(model * vec4(aPos, 1.0));
}