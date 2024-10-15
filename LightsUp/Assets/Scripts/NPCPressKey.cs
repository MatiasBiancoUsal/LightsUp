using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPressKey : MonoBehaviour
{
    public GameObject player;                // Referencia al jugador
    public GameObject targetObject;          // El objeto que quieres comprobar
    public float triggerDistance = 5f;       // Distancia mínima para activar el evento
    public KeyCode activationKey = KeyCode.E; // Tecla que activará el evento
    public Dialogue dialogue;                // El diálogo que quieres iniciar

    private DialogueManager dialogueManager; // El script que quieres llamar

    void Start()
    {
        if (targetObject != null)
        {
            dialogueManager = DialogueManager.Instance; // Usamos la instancia estática
        }
    }

    void Update()
    {
        if (player != null && targetObject != null && dialogueManager != null)
        {
            // Calcula la distancia entre el jugador y el objeto
            float distanceToTarget = Vector3.Distance(player.transform.position, targetObject.transform.position);

            // Comprueba si el jugador está dentro de la distancia especificada y si ha presionado la tecla
            if (distanceToTarget <= triggerDistance && Input.GetKeyDown(activationKey))
            {
                // Llama al método StartDialogue de DialogueManager
                if (!dialogueManager.isDialogueActive) // Solo iniciar diálogo si no está ya activo
                {
                    dialogueManager.StartDialogue(dialogue);
                }
            }
        }
    }
}
