using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isXTurn = true;

    private int _turnCount = 0;
    private TMP_Text _buttonLU;
    private TMP_Text _buttonMU;
    private TMP_Text _buttonRU;
    private TMP_Text _buttonLM;
    private TMP_Text _buttonMM;
    private TMP_Text _buttonRM;
    private TMP_Text _buttonLL;
    private TMP_Text _buttonML;
    private TMP_Text _buttonRL;

    [SerializeField] private Button[] _buttons;
    [SerializeField] private Button _resetButton;
    [SerializeField] private TMP_Text _displayText;

    // Start is called before the first frame update
    void Start()
    {
        _displayText.text = "X's Turn";
        _buttonLU = _buttons[0].GetComponentInChildren<TMP_Text>();
        _buttonMU = _buttons[1].GetComponentInChildren<TMP_Text>();
        _buttonRU = _buttons[2].GetComponentInChildren<TMP_Text>();
        _buttonLM = _buttons[3].GetComponentInChildren<TMP_Text>();
        _buttonMM = _buttons[4].GetComponentInChildren<TMP_Text>();
        _buttonRM = _buttons[5].GetComponentInChildren<TMP_Text>();
        _buttonLL = _buttons[6].GetComponentInChildren<TMP_Text>();
        _buttonML = _buttons[7].GetComponentInChildren<TMP_Text>();
        _buttonRL = _buttons[8].GetComponentInChildren<TMP_Text>();
    }

    public void OnButtonClick(int index)
    {
        // Increment _turnCount
        _turnCount++;

        TMP_Text buttonText = _buttons[index].GetComponentInChildren<TMP_Text>();

        // Put a red "X "or black "O" in button that was clicked
        if (isXTurn)
        {
            buttonText.text = "X";
            buttonText.color = Color.red;
            _displayText.text = "O's Turn";
        }
        else
        {
            buttonText.text = "O";
            buttonText.color = Color.black;
            _displayText.text = "X's Turn";
        }

        // Disable the button
        _buttons[index].enabled = false;

        if (!HasWinner())
        {
            if (_turnCount < 9)
            {
                // Set next turn
                isXTurn = !isXTurn;

                _displayText.text = (isXTurn ? "X's Turn" : "O's Turn");
            }
            else
            {
                // Draw game
                _displayText.text = "Draw game!";
            }
        }
        else
        {
            EndGame();
        }
    }

    public void ResetGame()
    {
        _turnCount = 0;
        _resetButton.gameObject.SetActive(false);
        _displayText.text = "X's Turn";

        foreach (var button in _buttons)
        {
            button.enabled = true;
            button.GetComponentInChildren<TMP_Text>().text = "";
        }
    }

    private bool HasWinner()
    {
        // Check for winner in 1st row in horizontal direction
        if (!string.IsNullOrEmpty(_buttonLU.text) && _buttonLU.text == _buttonMU.text && _buttonLU.text == _buttonRU.text)
        {
            DisplayWinner(_buttonLU.text);
            return true;
        }

        // Check for winner in 2nd row in horizontal direction
        if (!string.IsNullOrEmpty(_buttonLM.text) && _buttonLM.text == _buttonMM.text && _buttonLM.text == _buttonRM.text)
        {
            DisplayWinner(_buttonLM.text);
            return true;
        }

        // Check for winner in 3rd row in horizontal direction
        if (!string.IsNullOrEmpty(_buttonLL.text) && _buttonLL.text == _buttonML.text && _buttonLL.text == _buttonRL.text)
        {
            DisplayWinner(_buttonLL.text);
            return true;
        }

        // Check for winner in 1st column in vertical direction
        if (!string.IsNullOrEmpty(_buttonLU.text) && _buttonLU.text == _buttonLM.text && _buttonLU.text == _buttonLL.text)
        {
            DisplayWinner(_buttonLU.text);
            return true;
        }

        // Check for winner in 2nd column in vertical direction
        if (!string.IsNullOrEmpty(_buttonMU.text) && _buttonMU.text == _buttonMM.text && _buttonMU.text == _buttonML.text)
        {
            DisplayWinner(_buttonMU.text);
            return true;
        }

        // Check for winner in 3rd column in vertical direction
        if (!string.IsNullOrEmpty(_buttonRU.text) && _buttonRU.text == _buttonRM.text && _buttonRU.text == _buttonRL.text)
        {
            DisplayWinner(_buttonRU.text);
            return true;
        }

        // Check for winner starting in upper left in diagonal direction
        if (!string.IsNullOrEmpty(_buttonLU.text) && _buttonLU.text == _buttonMM.text && _buttonLU.text == _buttonRL.text)
        {
            DisplayWinner(_buttonLU.text);
            return true;
        }

        // Check for winner starting in upper right in diagonal direction
        if (!string.IsNullOrEmpty(_buttonRU.text) && _buttonRU.text == _buttonMM.text && _buttonRU.text == _buttonLL.text)
        {
            DisplayWinner(_buttonRU.text);
            return true;
        }

        return false;
    }

    private void DisplayWinner(string winner)
    {
        _displayText.text = winner + " wins!";
    }

    private void EndGame()
    {
        foreach (var button in _buttons)
        {
            button.enabled = false;
        }

        _resetButton.gameObject.SetActive(true);
    }
}
