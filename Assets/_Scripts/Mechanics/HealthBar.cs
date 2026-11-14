using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Billboard<HealthBar>
{
    [SerializeField] private Image healthImage;

    void Start()
    {
        if (GameManager.Instance == null)
            throw new MissingComponentException("GameManager instance cannot be found!");

        if (healthImage == null)
            throw new MissingComponentException("healthImage cannot be found!");
    }

    void Update()
    {
        float fillValue = (float)GameManager.Instance.currHealth / GameManager.Instance.maxHealth;

        healthImage.fillAmount = Mathf.Clamp01(fillValue);
    }
}