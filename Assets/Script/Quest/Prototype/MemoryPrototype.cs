using System.Collections.Generic;
using UnityEngine;

public class MemoryPrototype : MonoBehaviour {
    public static MemoryPrototype Instance;
    public List<MemoryFragment> memories;
    public int currentIndex;

    void Awake() { 
        Instance = this; 
    }

    public void UnlockNextMemory() {
        if (currentIndex >= memories.Count) return;

        memories[currentIndex].unlocked = true;
        Debug.Log("Memory Unlocked: " + memories[currentIndex].title);
        Debug.Log(memories[currentIndex].storyText);
        currentIndex++;
    }
}
