using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    // Ç«Ç±Ç≈Ç‡é¿çsÇ≈Ç´ÇÈÇ‚Ç¬
    public static SlotGenerator instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] GameObject slot;

    public void Spawn()
    {
        Instantiate(slot, this.transform);
    }
}
