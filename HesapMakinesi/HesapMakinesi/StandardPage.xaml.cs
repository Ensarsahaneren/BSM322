using System;
using Microsoft.Maui.Controls;

namespace HesapMakinesi
{
    public partial class StandardPage : ContentPage
    {
        private string currentInput = "";
        private double firstNumber = 0;
        private string currentOperator = "";
        private bool isNewEntry = false;

        public StandardPage()
        {
            InitializeComponent();
        }

        private void OnDigitClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (isNewEntry)
            {
                currentInput = "";
                isNewEntry = false;
            }
            currentInput += btn.Text;
            entryResult.Text = currentInput;
        }

        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!currentInput.Contains(","))
            {
                currentInput += ",";
                entryResult.Text = currentInput;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            currentInput = "";
            firstNumber = 0;
            currentOperator = "";
            entryResult.Text = "0";
        }

        private void OnClearEntryClicked(object sender, EventArgs e)
        {
            currentInput = "";
            entryResult.Text = "0";
        }

        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                entryResult.Text = string.IsNullOrEmpty(currentInput) ? "0" : currentInput;
            }
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (double.TryParse(currentInput.Replace(",", "."), out double number))
            {
                firstNumber = number;
                currentOperator = btn.Text;
                isNewEntry = true;

               
                lblHistory.Text = $"{firstNumber} {currentOperator}";
            }
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(currentInput.Replace(",", "."), out double secondNumber))
                return;

            double result = 0;
            bool error = false;

            switch (currentOperator)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "−":
                    result = firstNumber - secondNumber;
                    break;
                case "×":
                    result = firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber == 0)
                    {
                        error = true;
                        entryResult.Text = "0'a bölme hatası!";
                        lblHistory.Text = $"{firstNumber} ÷ {secondNumber}";
                    }
                    else
                    {
                        result = firstNumber / secondNumber;
                    }
                    break;
                case "%":
                    result = firstNumber % secondNumber;
                    break;
            }

            if (!error)
            {
                
                lblHistory.Text = $"{firstNumber} {currentOperator} {secondNumber} =";
                entryResult.Text = result.ToString("N");
                currentInput = result.ToString();
                isNewEntry = true;
            }
        }

        private void OnSpecialOperatorClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!double.TryParse(currentInput.Replace(",", "."), out double number))
                return;

            double result = 0;

            switch (btn.Text)
            {
                case "1/x":
                    if (number == 0)
                    {
                        entryResult.Text = "0'a bölme hatası!";
                        return;
                    }
                    result = 1 / number;
                    break;
                case "x²":
                    result = Math.Pow(number, 2);
                    break;
                case "²√x":
                    result = Math.Sqrt(number);
                    break;
            }

            entryResult.Text = result.ToString("N");
            currentInput = result.ToString();
            isNewEntry = true;
        }

        private void OnNegateClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentInput.Replace(",", "."), out double number))
            {
                number = -number;
                currentInput = number.ToString();
                entryResult.Text = currentInput;
            }
        }
    }
}
