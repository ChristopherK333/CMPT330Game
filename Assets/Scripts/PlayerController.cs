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

    public float mana;
    public float maxMana;
    public Image manaBar;

    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        maxHealth = health;
        maxMana = mana;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        manaBar.fillAmount = Mathf.Clamp(mana / maxMana, 0, 1);

        if (health <= 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }        
    }
}
