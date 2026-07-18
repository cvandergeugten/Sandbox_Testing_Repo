using UnityEngine;

public class StationInteractionManager : MonoBehaviour
{
    public SelectionManager selectionManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && selectionManager != null && selectionManager.SelectedUnit != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<InteractableStation>(out InteractableStation station))
                {
                    if (station.CanInteract(selectionManager.SelectedUnit))
                    {
                        station.Interact(selectionManager.SelectedUnit);
                    }
                    else
                    {
                        Debug.Log("Move closer to " + station.stationName + " to interact.");
                    }
                }
            }
        }
    }
}
