using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Configuração das Cenas")]
    [SerializeField] private string NomeCenaJogo;
    [SerializeField] private string NomeCenaInicial;

    [Header("Configuração do Fade")]
    [SerializeField] private CanvasGroup painelFade;
    [SerializeField] private float duracaoFade = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Iniciar()
    {
        StartCoroutine(FadeOutEChangeScene(NomeCenaJogo));
    }

    public void Voltar()
    {
        StartCoroutine(FadeOutEChangeScene(NomeCenaInicial));
    }

    public void Fechar()
    {
        StartCoroutine(FadeOutEQuit());
    }


    IEnumerator FadeIn()
    {
        painelFade.alpha = 1; 
        painelFade.blocksRaycasts = true;

        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracaoFade)
        {
            tempoDecorrido += Time.deltaTime;
           
            painelFade.alpha = Mathf.Lerp(1f, 0f, tempoDecorrido / duracaoFade);
            yield return null;
        }

        painelFade.alpha = 0;
        painelFade.blocksRaycasts = false;
    }

    IEnumerator FadeOutEChangeScene(string nomeDaCena)
    {
        painelFade.blocksRaycasts = true;

        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracaoFade)
        {
            tempoDecorrido += Time.deltaTime;
            painelFade.alpha = Mathf.Lerp(0f, 1f, tempoDecorrido / duracaoFade);
            yield return null;
        }

        painelFade.alpha = 1;
        
        SceneManager.LoadScene(nomeDaCena);
    }

    IEnumerator FadeOutEQuit()
    {
        float tempoDecorrido = 0f;
        painelFade.blocksRaycasts = true;

        while (tempoDecorrido < duracaoFade)
        {
            tempoDecorrido += Time.deltaTime;
            painelFade.alpha = Mathf.Lerp(0f, 1f, tempoDecorrido / duracaoFade);
            yield return null;
        }
        
        Application.Quit();
    }
}