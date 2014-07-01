#version 400
precision highp float; 

layout (location = 0) in vec3 vert; 
layout (location = 1) in vec4 color; 
uniform mat4 MVP;
out vec4 outColor;
void main(void) 
{ 
    gl_Position =  (MVP) * vec4(vert, 1);
	outColor = color;
}