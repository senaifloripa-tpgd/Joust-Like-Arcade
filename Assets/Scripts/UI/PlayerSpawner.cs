using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Prefabs dos Jogadores")]
    public GameObject prefabJogador1;
    public GameObject prefabJogador2;

    [Header("Pontos de Nascimento (Transform)")]
    public Transform pontoNascimentoJ1;
    public Transform pontoNascimentoJ2;

    void Start()
    {
        ConfigurarPartida();
    }

    private void ConfigurarPartida()
    {
        // O Jogador 1 sempre será criado, independente da escolha
        if (prefabJogador1 != null && pontoNascimentoJ1 != null)
        {
            Instantiate(prefabJogador1, pontoNascimentoJ1.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Faltou referenciar o Prefab ou o Ponto de Nascimento do Jogador 1!");
        }

        // Se a opção de 2 jogadores foi selecionada no menu, criamos o Jogador 2
        if (GameSettings.NumeroDeJogadores == 2)
        {
            if (prefabJogador2 != null && pontoNascimentoJ2 != null)
            {
                Instantiate(prefabJogador2, pontoNascimentoJ2.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Faltou referenciar o Prefab ou o Ponto de Nascimento do Jogador 2!");
            }
        }
    }
}