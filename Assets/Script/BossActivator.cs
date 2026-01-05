using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossObject;  
    public MonoBehaviour bossAI;
    public Collider2D bossCollider;
    public Animator bossAnimator;

    bool activated = false;

    void Start()
    {
        DeactivateBoss();
    }

    void Update()
    {
        if (activated) return;
        if (QuestManager.Instance == null) return;

        if (QuestManager.Instance.currentQuestIndex >= 1)
        {
            ActivateBoss();
        }
    }

    void DeactivateBoss()
    {
        bossObject.SetActive(false);
    }

    void ActivateBoss()
    {
        activated = true;

        bossObject.SetActive(true);

        if (bossAI != null) bossAI.enabled = true;
        if (bossCollider != null) bossCollider.enabled = true;
        if (bossAnimator != null) bossAnimator.enabled = true;

        Debug.Log("Boss Activated!");
    }
}
