using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour {
    public static MemoryManager Instance;
    public List<MemoryFragment> memories;
    public int currentIndex;

    void Awake() { 
        Instance = this; 
    }

    public void UnlockNextMemory() {
        if (currentIndex >= memories.Count) return;

        memories[currentIndex].unlocked = true;
        MemoryUI.Instance.ShowMemory(memories[currentIndex]);
        currentIndex++;
    }
}
