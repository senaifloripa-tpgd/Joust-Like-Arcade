using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // O Singleton permite chamarmos esse script de qualquer lugar do código
    public static TransitionManager Instancia;

    [Header("Configurações")]
    public CanvasGroup grupoDeTransicao;
    public float tempoDeFade = 1f;
    public float tempoMostrandoLogo = 1.5f;

    private void Awake()
    {
        // Configuração do Singleton e proteção contra duplicação
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); // Impede que o Canvas seja destruído ao mudar de cena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Função que será chamada pelo botão do menu
    public void IniciarTransicao(string nomeDaCena)
    {
        StartCoroutine(RotinaDeTransicao(nomeDaCena));
    }

    private IEnumerator RotinaDeTransicao(string nomeDaCena)
    {
        // 1. Bloqueia cliques do mouse para o jogador não apertar botões durante o fade
        grupoDeTransicao.blocksRaycasts = true;

        // 2. Fade-In (Tela escurece, logo aparece)
        float tempo = 0f;
        while (tempo < tempoDeFade)
        {
            tempo += Time.deltaTime;
            grupoDeTransicao.alpha = tempo / tempoDeFade;
            yield return null; // Espera o próximo frame
        }
        grupoDeTransicao.alpha = 1f;

        // 3. Aguarda um tempo com a tela preta e a logo aparecendo
        yield return new WaitForSeconds(tempoMostrandoLogo);

        // 4. Inicia o carregamento da cena do jogo em segundo plano
        AsyncOperation carregamentoAssincrono = SceneManager.LoadSceneAsync(nomeDaCena);
        
        // Espera a cena terminar de carregar totalmente
        while (!carregamentoAssincrono.isDone)
        {
            yield return null;
        }

        // 5. Fade-Out (Tela clareia, revelando a cena do jogo já carregada)
        tempo = 0f;
        while (tempo < tempoDeFade)
        {
            tempo += Time.deltaTime;
            // Aqui fazemos a conta inversa: de 1 para 0
            grupoDeTransicao.alpha = 1f - (tempo / tempoDeFade);
            yield return null;
        }
        
        // 6. Finaliza a transição, liberando a tela
        grupoDeTransicao.alpha = 0f;
        grupoDeTransicao.blocksRaycasts = false;
    }
}