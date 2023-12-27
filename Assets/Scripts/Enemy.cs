using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _goldReward = 25;
    [SerializeField] private int _goldPenalty = 25;

    private Bank _bank;

    private void Start()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if( _bank == null)
        {
            return;
        }

        _bank.Deposit(_goldReward);
    }

    public void SteelGold()
    {
        if (_bank == null)
        {
            return;
        }

        _bank.Withdraw(_goldPenalty);
    }
}