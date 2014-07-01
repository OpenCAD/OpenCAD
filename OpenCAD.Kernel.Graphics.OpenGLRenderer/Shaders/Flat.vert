#version 400
precision highp float; 
layout (location = 0) in vec2 vert; 

out vec2 vertTexcoord;

void main(void) 
{ 
    gl_Position = vec4(vert, 0.0, 1.0);
	vertTexcoord = (vert + 1.0) / 2.0;
}