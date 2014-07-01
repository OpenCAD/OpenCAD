#version 400
uniform sampler2D texture;
uniform float offset;
in vec2 vertTexcoord;
void main() {
	vec2 texcoord = vertTexcoord;
	texcoord.x += sin(texcoord.y * 4*2*3.14159 + offset) / 100;
    vec4 diffuseColor = texture2D(texture, texcoord);
    vec4 invertColor = 1.0 - diffuseColor;
    gl_FragColor = vec4(invertColor.rgb, diffuseColor.a);
}
