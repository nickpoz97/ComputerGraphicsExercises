#version 330 core
in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoords;

out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};
uniform Material material;

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
uniform Light light;  

uniform vec3 viewPos;

void main()
{
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;

    vec3 n = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);

    float distanceFactor = 1 / max((pow(length(light.position - FragPos), 2) * 0.3), 0.3);

    vec3 diffuse = texture(material.diffuse, TexCoords).rgb * light.diffuse * max(dot(lightDir, n), 0.0) * distanceFactor;

    vec3 viewDir = normalize(viewPos - FragPos);
    // compute reflection vector (using vector FROM light source TO fragPos)
    vec3 reflectDir = reflect(-lightDir, n);
    // cos(alpha) ** 32
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (vec3(1.0f) - texture(material.specular, TexCoords).rgb) * spec * distanceFactor;

    vec3 result = ambient + diffuse + specular;

    FragColor = vec4(result, 1.0);
}