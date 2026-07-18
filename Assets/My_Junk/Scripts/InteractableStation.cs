using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableStation : MonoBehaviour
{
    public string stationName = "Station";
    [Range(0.5f, 10f)]
    public float interactionRange = 3f;
    [Range(0.1f, 10f)]
    public float interactionResetDelay = 2f;
    public bool useInteractionColorChange = true;
    public Color highlightColor = Color.yellow;
    public Color interactionColor = Color.green;

    private Renderer[] renderers;
    private Color[] originalColors;
    private GameObject actorBeingColored;
    private Color[] actorOriginalColors;
    private bool hasStoredActorColors;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    void OnMouseEnter()
    {
        SetHighlight(true);
    }

    void OnMouseExit()
    {
        SetHighlight(false);
    }

    private void SetHighlight(bool highlight)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = highlight ? highlightColor : originalColors[i];
        }
    }

    public bool CanInteract(GameObject actor)
    {
        if (actor == null)
            return false;

        return Vector3.Distance(transform.position, actor.transform.position) <= interactionRange;
    }

    private void StoreActorColors(GameObject actor)
    {
        if (actor == null)
            return;

        Renderer[] actorRenderers = actor.GetComponentsInChildren<Renderer>();
        actorOriginalColors = new Color[actorRenderers.Length];
        for (int i = 0; i < actorRenderers.Length; i++)
        {
            actorOriginalColors[i] = actorRenderers[i].material.color;
        }

        actorBeingColored = actor;
        hasStoredActorColors = true;
    }

    protected void ApplyInteractionColor(GameObject actor, Color color)
    {
        if (actor == null)
            return;

        if (!hasStoredActorColors || actorBeingColored != actor)
        {
            StoreActorColors(actor);
        }

        Renderer[] actorRenderers = actor.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < actorRenderers.Length; i++)
        {
            actorRenderers[i].material.color = color;
        }
    }

    private void ResetActorColor()
    {
        if (actorBeingColored == null || !hasStoredActorColors)
            return;

        Renderer[] actorRenderers = actorBeingColored.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < actorRenderers.Length; i++)
        {
            if (i < actorOriginalColors.Length)
            {
                actorRenderers[i].material.color = actorOriginalColors[i];
            }
        }

        actorBeingColored = null;
        actorOriginalColors = new Color[0];
        hasStoredActorColors = false;
    }

    public virtual void Interact(GameObject actor)
    {
        Debug.Log(actor.name + " interacted with " + stationName);

        if (actor != null && actor.CompareTag("Test_Dummy") && useInteractionColorChange)
        {
            ApplyInteractionColor(actor, interactionColor);
            Invoke(nameof(ResetActorColor), interactionResetDelay);
        }
    }
}
