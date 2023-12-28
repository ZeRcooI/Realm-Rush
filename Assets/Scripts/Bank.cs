using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 150;
    [SerializeField] private int _currentBalance;
    [SerializeField] private TextMeshProUGUI _displayBalance;

    private string _textBalance = "Gold: ";

    public int CurrentBalance => _currentBalance;

    private void Awake()
    {
        _currentBalance = _startingBalance;

        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);

        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);

        UpdateDisplay();

        if(_currentBalance < 0 )
        {
            ReloadScene();
        }
    }

    private void UpdateDisplay()
    {
        _displayBalance.text = _textBalance + _currentBalance;
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}