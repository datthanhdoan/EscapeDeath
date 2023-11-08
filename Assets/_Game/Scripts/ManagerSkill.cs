using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSkill : MonoBehaviour
{
    public GameObject skill_1, skill_2;
    private bool canUseSkill_1 = true, canUseSkill_2 = true;
    private float delayTimeSkill_1 = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill_1)
        {
            StartCoroutine(ActivateAndDeactivateSkill(skill_1, 4f));
            canUseSkill_1 = false;
            Invoke(nameof(ResetSkillCooldown1), 10f);
        }
        if (Input.GetKeyDown(KeyCode.R) && canUseSkill_2)
        {
            StartCoroutine(ActivateAndDeactivateSkill(skill_2, 4f));
            canUseSkill_2 = false;
            Invoke(nameof(ResetSkillCooldown1), 10f);
        }
    }
    private void ResetSkillCooldown1()
    {
        canUseSkill_1 = true;
    }
    private IEnumerator ActivateAndDeactivateSkill(GameObject skill, float delayTime)
    {
        skill.SetActive(true);
        yield return new WaitForSeconds(delayTime); // Adjust this delay as per your requirement
        skill.SetActive(false);
    }
}
