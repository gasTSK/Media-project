using UnityEngine;

// Attach this script to every NPC in your scene.
// Fill in their name and dialogue lines in the Inspector.
public class NPCDialogue : MonoBehaviour
{
    [Header("NPC Info")]
    public string npcName;         // shown at top of dialogue box
    public string[] dialogueLines; // all the lines this NPC says

    private bool playerInRange = false;

    void Update()
    {
        // Player presses E while standing next to NPC
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(npcName, dialogueLines);
        }
    }

    // When player walks into the NPC's trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // When player walks away
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // NOTE: If your game is 3D, replace OnTriggerEnter2D with OnTriggerEnter
    // and OnTriggerExit2D with OnTriggerExit
}
