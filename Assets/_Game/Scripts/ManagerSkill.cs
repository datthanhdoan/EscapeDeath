using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSkill : MonoBehaviour
{
    public GameObject skill_1;
    bool canUseSkill_1 = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canUseSkill_1)
        {
            StartCoroutine(ActivateAndDeactivateSkill());
            canUseSkill_1 = false;
            Invoke(nameof(ResetSkillCooldown1), 10f);
        }
    }
    private void ResetSkillCooldown1()
    {
        canUseSkill_1 = true;
    }
    private IEnumerator ActivateAndDeactivateSkill()
    {
        skill_1.SetActive(true);
        yield return new WaitForSeconds(4f); // Adjust this delay as per your requirement
        skill_1.SetActive(false);
    }
}
