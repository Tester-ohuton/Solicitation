using UnityEngine;

public class Cardboard : MonoBehaviour
{
    private int indexFlag; // Unique index for each cardboard

    public void SetCardboardStatus(int idx)
    {
        indexFlag = idx;
    }

    public int GetCardboardStatus()
    {
        return indexFlag;
    }

    public void OnMouseDown()
    {
        Debug.Log("Cardboard " + indexFlag + " clicked!");
        // Add additional logic for when the cardboard is clicked
    }
}
