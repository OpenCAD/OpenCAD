#version 400
precision highp float; 

struct material
{
  vec4 diffuse;
};
material mymaterial = material(vec4(1.0, 0.8, 0.8, 1.0));


in FragmentData
{
    vec3 normal;
    vec4 color;
} frag;

void main()
{
    gl_FragColor = vec4(abs(frag.normal.x), abs(frag.normal.y), abs(frag.normal.z), 1);
    gl_FragColor = frag.color;
}