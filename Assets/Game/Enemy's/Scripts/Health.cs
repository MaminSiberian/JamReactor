using UnityEngine;

internal class Health : MonoBehaviour, IHP
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int damage)
    {
        if (currentHP - damage > 0)
        {
            currentHP -= damage;
        }
        else
        {
            Debug.Log("gameover");
        }
    }
}