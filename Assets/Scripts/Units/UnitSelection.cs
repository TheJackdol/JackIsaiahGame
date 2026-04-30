using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public GameObject selectionCircle;

    public void Select()
    {
        if (selectionCircle != null)
            selectionCircle.SetActive(true);
    }

    public void Deselect()
    {
        if (selectionCircle != null)
            selectionCircle.SetActive(false);
    }
}