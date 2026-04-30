using System.Collections.Generic;
using UnityEngine;

public class RTSController : MonoBehaviour
{
    public Camera cam;
    private bool isAttackMoveCommand = false;
    private List<UnitSelection> selectedSelections = new List<UnitSelection>();
    private List<UnitMovement> selectedUnits = new List<UnitMovement>();

    private Vector2 startPos;
    private Vector2 endPos;
    

    void HandleStopInput()
{
    if (Input.GetKeyDown(KeyCode.Q))
    {
        foreach (UnitMovement unit in selectedUnits)
        {
            unit.Stop();
        }

        Debug.Log("Stopped units with Q");
    }
}


    void Update()
    {
        HandleSelectionBox();
        HandleMovement();
        HandleAttackInput();
        HandleStopInput();
        if (Input.GetKeyDown(KeyCode.Z))
{
    isAttackMoveCommand = true;
}
    }

    void HandleSelectionBox()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = cam.ScreenToWorldPoint(Input.mousePosition);
            SelectUnits();
        }
    }

    void SelectUnits()
{
    // Deselect old units
    foreach (UnitSelection sel in selectedSelections)
    {
        sel.Deselect();
    }

    selectedSelections.Clear();
    selectedUnits.Clear();

    Vector2 min = Vector2.Min(startPos, endPos);
    Vector2 max = Vector2.Max(startPos, endPos);

    Collider2D[] hits = Physics2D.OverlapAreaAll(min, max);

    foreach (Collider2D hit in hits)
    {
        if (hit.CompareTag("Unit"))
        {
            UnitMovement unit = hit.GetComponent<UnitMovement>();
            UnitSelection selection = hit.GetComponent<UnitSelection>();

            if (unit != null && selection != null)
            {
                selectedUnits.Add(unit);
                selectedSelections.Add(selection);
                selection.Select();
            }
        }
    }
}

    void HandleMovement()
    {
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);

            foreach (UnitMovement unit in selectedUnits)
            {
                unit.MoveTo(target, isAttackMoveCommand);
                isAttackMoveCommand = false;
            }
        }
    }

    void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && selectedUnits.Count > 0)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                foreach (UnitMovement unit in selectedUnits)
                {
                    unit.Attack(hit.collider.gameObject);
                }

                Debug.Log("Attacking enemy!");
            }
            else
            {
                Debug.Log("No enemy under cursor");
            }
        }
    }
}