using UnityEngine;
using UnityEngine.Rendering; // Necessário para acessar o Volume de Pós-Processamento

public class RetroModeController : MonoBehaviour
{
    [Header("Pós-Processamento")]
    public Volume postProcessingVolume; // Arraste o Volume global da cena aqui

    [Header("Shader do Renderer2D")]
    public Material materialDoShaderRetro; // O material que tem o shader de scanlines/CRT
    
    [Tooltip("O nome exato da variável de densidade dentro do seu Shader (ex: _Density ou _LineDensity)")]
    public string nomePropriedadeDensidade = "_Density";
    
    [Tooltip("O nome exato da variável de velocidade dentro do seu Shader (ex: _Speed ou _LineSpeed)")]
    public string nomePropriedadeVelocidade = "_Speed";

    [Header("Valores Originais (Quando o Modo Retrô está ATIVADO)")]
    public float densidadeOriginal = 1.0f;
    public float velocidadeOriginal = 0.5f;

    void Start()
    {
        AplicarConfiguracaoRetro();
    }

    public void AplicarConfiguracaoRetro()
    {
        bool modoAtivo = GameSettings.RetroModeAtivo;

        // 1. Controla o Pós-Processamento
        if (postProcessingVolume != null)
        {
            // Ativa ou desativa o volume de pós-processamento inteiro
            postProcessingVolume.enabled = modoAtivo;
        }

        // 2. Controla os valores de Densidade e Velocidade do Shader
        if (materialDoShaderRetro != null)
        {
            if (modoAtivo)
            {
                // Se estiver ativo, devolve os valores padrão do efeito retrô
                materialDoShaderRetro.SetFloat(nomePropriedadeDensidade, densidadeOriginal);
                materialDoShaderRetro.SetFloat(nomePropriedadeVelocidade, velocidadeOriginal);
            }
            else
            {
                // Se estiver desativado, zera as propriedades conforme solicitado
                materialDoShaderRetro.SetFloat(nomePropriedadeDensidade, 0f);
                materialDoShaderRetro.SetFloat(nomePropriedadeVelocidade, 0f);
            }
        }
        else
        {
            Debug.LogWarning("Material do Shader Retrô não foi referenciado no RetroModeController!");
        }
    }
}