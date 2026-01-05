using UnityEngine;

public class BossActivator : MonoBehaviour
{
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

        if (QuestManager.Instance.currentQuestIndex >= 3)
        {
            ActivateBoss();
        }
    }

    void DeactivateBoss()
    {
        if (bossAI != null) bossAI.enabled = false;
        if (bossCollider != null) bossCollider.enabled = false;
        if (bossAnimator != null) bossAnimator.enabled = false;

        gameObject.SetActive(false);   // boss benar-benar hilang
    }

    void ActivateBoss()
    {
        activated = true;

        gameObject.SetActive(true);

        if (bossAI != null) bossAI.enabled = true;
        if (bossCollider != null) bossCollider.enabled = true;
        if (bossAnimator != null) bossAnimator.enabled = true;

        Debug.Log("Boss Activated!");
    }
}
