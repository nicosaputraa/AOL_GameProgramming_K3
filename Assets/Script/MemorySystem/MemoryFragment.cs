using UnityEngine;

[System.Serializable]
public class MemoryFragment {
    public string title;
    [TextArea(3,10)]
    public string storyText;
    public bool unlocked;
}

