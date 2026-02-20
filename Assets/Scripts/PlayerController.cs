using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;

    public float stamina;
    public float maxStam;
    public Image stamBar;

    public float mana;
    public float maxMana;
    public Image manaBar;

    public Coroutine stamRecharge;
    public float stamChargeRate;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxStam = stamina;
        maxMana = mana;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        stamBar.fillAmount = Mathf.Clamp(stamina / maxStam, 0, 1);
        manaBar.fillAmount = Mathf.Clamp(mana / maxMana, 0, 1);

        if (health <= 0)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }

        stamRecharge = StartCoroutine(RechargeStam());
    }

    private IEnumerator RechargeStam()
    {
        yield return new WaitForSeconds(1f);

        while (stamina < maxStam)
        {
            stamina += stamChargeRate / 5f;
            if (stamina > maxStam)
            {
                stamina = maxStam;
            }

            stamBar.fillAmount = Mathf.Clamp(stamina / maxStam, 0, 1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
