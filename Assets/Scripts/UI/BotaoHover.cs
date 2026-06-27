using UnityEngine;
using UnityEngine.EventSystems; // Necessário para detectar o mouse na UI

public class BotaoHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Configuracoes")]
    public float escalaDoHover = 1.2f;
    
    public float velocidade = 10f;

    private Vector3 escalaOriginal;
    private Vector3 escalaAlvo;

    void Start()
    {
        
        escalaOriginal = transform.localScale;
        escalaAlvo = escalaOriginal;
    }

    void Update()
    {
        
        transform.localScale = Vector3.Lerp(transform.localScale, escalaAlvo, Time.deltaTime * velocidade);
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        escalaAlvo = escalaOriginal * escalaDoHover;
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        escalaAlvo = escalaOriginal;
    }
}