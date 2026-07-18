using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableStation : MonoBehaviour
{
    public string stationName = "Station";
    public float interactionRange = 2f;
    public Color highlightColor = Color.yellow;

    private Renderer[] renderers;
    private Color[] originalColors;

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

    public virtual void Interact(GameObject actor)
    {
        Debug.Log(actor.name + " interacted with " + stationName);
    }
}
