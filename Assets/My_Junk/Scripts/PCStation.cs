using UnityEngine;

public class PCStation : InteractableStation
{
    [Tooltip("What the station is ordering.")]
    public string orderName = "Supplies";

    [Tooltip("How many units are requested for the order.")]
    public int orderAmount = 1;

    public bool orderPlaced { get; private set; }

    public override void Interact(GameObject actor)
    {
        base.Interact(actor);

        if (actor == null)
            return;

        orderPlaced = true;
        Debug.Log(actor.name + " ordered " + orderAmount + " " + orderName + " from the " + stationName + ".");
    }
}
