#version 400
precision highp float; 
layout (location = 0) in vec2 vert; 
layout (location = 1) in vec2 tex; 

out vec2 outTex;
void main(void) 
{ 
	outTex = tex;
    gl_Position = vec4(vert, 0.0, 1.0);
}