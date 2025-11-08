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
}
