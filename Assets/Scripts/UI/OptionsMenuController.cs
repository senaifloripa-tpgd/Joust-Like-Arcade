using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [Header("Componentes de UI")]
    public Toggle toggleRetroMode;

    void Start()
    {
        if (toggleRetroMode != null)
        {
            // Sincroniza o visual do Toggle com o valor salvo
            toggleRetroMode.isOn = GameSettings.RetroModeAtivo;

            // Vincula a função que roda quando o jogador clica no Toggle
            toggleRetroMode.onValueChanged.AddListener(SetRetroMode);
        }
    }

    public void SetRetroMode(bool ativado)
    {
        GameSettings.RetroModeAtivo = ativado;

        // Opcional: Se o seu menu de opções puder ser aberto dentro da cena de jogo,
        // isso atualiza o efeito visual em tempo real.
        RetroModeController controladorVisual = FindFirstObjectByType<RetroModeController>();
        if (controladorVisual != null)
        {
            controladorVisual.AplicarConfiguracaoRetro();
        }
    }
}