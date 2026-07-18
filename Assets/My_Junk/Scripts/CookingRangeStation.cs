using UnityEngine;

public class CookingRangeStation : InteractableStation
{
    [Tooltip("What this station is cooking.")]
    public string recipeName = "Dish";

    [Tooltip("How long the cooking state lasts before finishing.")]
    public float cookTime = 3f;

    public bool isCooking { get; private set; }

    public override void Interact(GameObject actor)
    {
        base.Interact(actor);

        if (actor == null)
            return;

        isCooking = true;
        CancelInvoke(nameof(FinishCooking));
        Invoke(nameof(FinishCooking), cookTime);

        Debug.Log(actor.name + " started cooking " + recipeName + " at the " + stationName + ".");
    }

    private void FinishCooking()
    {
        isCooking = false;
        Debug.Log(recipeName + " is ready from the " + stationName + ".");
    }
}
