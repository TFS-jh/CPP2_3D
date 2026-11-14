using UnityEngine;

public class HealthBar : Billboard<HealthBar>
{
    [SerializeField] private int maxHealth = 150;
    private int currHealth = 150;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
            Debug.Log("Max Health cannot be exceeded!");
        }

        if (currHealth < 0)
        {
            currHealth = 0;
            Debug.Log("currHealth cannot go below 0!");
        }
    }
}
