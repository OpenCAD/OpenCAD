#version 400
precision highp float; 
layout (location = 0) in vec2 vert; 
layout (location = 1) in vec4 colour; 

out vec4 outColour;

void main(void) 
{ 
    gl_Position = vec4(vert, 0.0, 1.0);
	outColour = colour;
}