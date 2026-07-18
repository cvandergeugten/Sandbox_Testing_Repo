using UnityEngine;

public class CookingStation : InteractableStation
{
    public override void Interact(GameObject actor)
    {
        base.Interact(actor);
        Debug.Log(actor.name + " is now using the cooking station.");
        // Add Overcooked-style behavior here, e.g. start a timer, change food state, etc.
    }
}
