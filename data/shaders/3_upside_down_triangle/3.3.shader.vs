#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aCol;

out vec3 ourColor;
uniform float xOffset;

void main(){
	gl_Position = vec4(aPos, 1.0);
	gl_Position.x += xOffset;

	ourColor = aCol;
	ourColor.r = (gl_Position.x + 1.0) / 2.0;
}