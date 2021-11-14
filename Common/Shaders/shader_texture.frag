#version 330

in vec2 texCoord;
uniform sampler2D texture0;

out vec4 outputColor;

void main()
{
    outputColor = texture(texture0, texCoord);
}