#version 330 core
in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
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

    vec3 n = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);

    float distance = length(light.position - FragPos);
    float distanceFactor = 1 / (1.0 + 0.2 * distance + 0.6 * pow(distance, 2));

    vec3 ambient = material.ambient * light.ambient * distanceFactor;
    vec3 diffuse = material.diffuse * light.diffuse * max(dot(lightDir, n), 0.0) * distanceFactor;

    vec3 viewDir = normalize(viewPos - FragPos);
    // compute reflection vector (using vector FROM light source TO fragPos)
    vec3 reflectDir = reflect(-lightDir, n);
    // cos(alpha) ** 32
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * material.specular * spec * distanceFactor;

    vec3 result = ambient + diffuse + specular;

    FragColor = vec4(result, 1.0);
}