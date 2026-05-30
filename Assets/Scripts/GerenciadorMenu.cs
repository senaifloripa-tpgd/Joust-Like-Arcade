using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorMenu : MonoBehaviour
{

    public void Iniciar()
    {
        SceneManager.LoadScene("UITESTE 2");
    }

    public void Sair()
    {
        Debug.Log("Você saiu!");
    }

    public void Pontuacao()
    {
        SceneManager.LoadScene("UITESTE 3");
    }
}
