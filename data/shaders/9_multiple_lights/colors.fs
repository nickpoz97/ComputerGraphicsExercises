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
uniform Light light[3];  
uniform vec3 viewPos;

void main()
{
    vec3 result = vec3(0.0);

    for (int i = 0 ; i < 3 ; i ++){
        vec3 ambient = light[i].ambient * texture(material.diffuse, TexCoords).rgb;

        vec3 n = normalize(Normal);
        vec3 lightDir = normalize(light[i].position - FragPos);
        float distance = length(light[i].position - FragPos);
        //float distanceFactor = 1.0 ;
        float distanceFactor = 1.0 / (1.0 + 0.3 * distance * 0.3 * pow(distance, 2));

        vec3 diffuse = texture(material.diffuse, TexCoords).rgb * light[i].diffuse * max(dot(lightDir, n), 0.0) * distanceFactor;

        vec3 viewDir = normalize(viewPos - FragPos);
        // compute reflection vector (using vector FROM light source TO fragPos)
        vec3 reflectDir = reflect(-lightDir, n);
        // cos(alpha) ** 32
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
        vec3 specular = light[i].specular * (texture(material.specular, TexCoords).rgb) * spec * distanceFactor;

        result += ambient + diffuse + specular;
    }
    FragColor = vec4(result, 1.0);
}