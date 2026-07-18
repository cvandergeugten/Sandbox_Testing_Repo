using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private GameObject selectedUnit;
    public GameObject SelectedUnit => selectedUnit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag == "Test_Dummy")
                {
                    selectedUnit = hitObject;
                    Debug.Log("Selected unit: " + selectedUnit.name);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print("Right-clicked on: " + hit.collider.gameObject.name);
                MoveSelectedUnitToTerrain();
            }
        }
    }

    private void MoveSelectedUnitToTerrain()
    {
        if (selectedUnit == null)
        {
            Debug.LogWarning("No unit selected.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                Vector3 targetPosition = hit.point + new Vector3(0, 1, 0);
                if (selectedUnit.TryGetComponent<UnitMovement>(out UnitMovement movement))
                {
                    movement.MoveTo(targetPosition);
                    //Debug.Log("Moving unit to: " + targetPosition);
                }
                else
                {
                   // Debug.LogWarning("Selected unit does not have UnitMovement component.");
                }
            }
            else
            {
                Debug.LogWarning("Hit object is not terrain.");
            }
        }
        else
        {
            Debug.LogWarning("No object hit by raycast.");
        }
    }
}
