using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;
    [SerializeField] private int _currentBalance;

    public int CurrentBalance => _currentBalance;

    private void Awake()
    {
        _currentBalance = _startingBalance;
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
    }
}