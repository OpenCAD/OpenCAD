#version 400

in vec4 outColour;

void main() {

    gl_FragColor = vec4(0.137, 0.121, 0.125, 1);
	gl_FragColor = outColour;
}
