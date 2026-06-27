using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Configurações de Cena")]
    [Tooltip("Digite o nome exato da sua cena de jogo aqui")]
    public string nomeDaCenaDeJogo = "GameScene";

    [Header("Painéis de UI (Canvas)")]
    public GameObject painelMenuPrincipal;
    public GameObject painelOpcoes;

    private void Start()
    {
        // Garante o estado inicial correto ao abrir o jogo
        if (painelMenuPrincipal != null) painelMenuPrincipal.SetActive(true);
        if (painelOpcoes != null) painelOpcoes.SetActive(false);
    }

    // --- Fluxo de Jogo ---
    
    public void IniciarUmJogador()
    {
        GameSettings.NumeroDeJogadores = 1;
        CarregarJogo();
    }

    public void IniciarDoisJogadores()
    {
        GameSettings.NumeroDeJogadores = 2;
        CarregarJogo();
    }

    private void CarregarJogo()
    {
        if (TransitionManager.Instancia != null)
        {
            TransitionManager.Instancia.IniciarTransicao(nomeDaCenaDeJogo);
        }
        else
        {
            // Fallback caso teste a cena sem o TransitionManager
            UnityEngine.SceneManagement.SceneManager.LoadScene(nomeDaCenaDeJogo);
        }
    }

    // --- Navegação de Menus ---

    public void AbrirOpcoes()
    {
        if (painelMenuPrincipal != null && painelOpcoes != null)
        {
            painelMenuPrincipal.SetActive(false);
            painelOpcoes.SetActive(true);
        }
    }

    public void FecharOpcoes()
    {
        if (painelMenuPrincipal != null && painelOpcoes != null)
        {
            painelOpcoes.SetActive(false);
            painelMenuPrincipal.SetActive(true);
        }
    }

    // --- Sair ---
    
    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}