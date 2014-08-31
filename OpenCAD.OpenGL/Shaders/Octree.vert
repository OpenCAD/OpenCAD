#version 400
precision highp float; 

layout (location = 0) in vec3 vert; 
layout (location = 1) in vec4 colour; 
layout (location = 2) in float size; 


uniform mat4 MVP;

out Data
{
  vec4 color;
  float size;
} v;

void main(void) 
{ 
    gl_Position = (MVP) * vec4(vert, 1.0);
    v.size = size;
    v.color = colour;
}