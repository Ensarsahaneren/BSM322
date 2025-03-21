using System;
using Microsoft.Maui.Controls;

namespace HesapMakinesi
{
    public partial class ScientificPage : ContentPage
    {
        private string currentInput = "";
        private double firstNumber = 0;
        private string currentOperator = "";
        private bool isNewEntry = false;

        public ScientificPage()
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

        private void OnClearClicked(object sender, EventArgs e)
        {
            currentInput = "";
            firstNumber = 0;
            currentOperator = "";
            entryResult.Text = "0";
        }

        private void OnNegateClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentInput, out double value))
            {
                value = -value;
                currentInput = value.ToString();
                entryResult.Text = currentInput;
            }
        }

        private void OnConstantClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "π")
                currentInput = Math.PI.ToString();
            else if (btn.Text == "e")
                currentInput = Math.E.ToString();

            entryResult.Text = currentInput;
            isNewEntry = true;
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


        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!currentInput.Contains(","))
            {
                currentInput += ",";
                entryResult.Text = currentInput;
            }
        }
        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                entryResult.Text = string.IsNullOrEmpty(currentInput) ? "0" : currentInput;
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
                case "mod":
                    result = firstNumber % secondNumber;
                    break;
                case "xʸ":
                    result = Math.Pow(firstNumber, secondNumber);
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
            if (!double.TryParse(currentInput, out double value))
                return;

            double result = 0;
            bool error = false;

            switch (btn.Text)
            {
                case "x²":
                    result = Math.Pow(value, 2);
                    break;
                case "1/x":
                    if (value == 0)
                    {
                        entryResult.Text = "0'a bölme hatası!";
                        error = true;
                    }
                    else
                        result = 1 / value;
                    break;
                case "|x|":
                    result = Math.Abs(value);
                    break;
                case "√x":
                    result = Math.Sqrt(value);
                    break;
                case "exp":
                    result = Math.Exp(value);
                    break;
                case "10ˣ":
                    result = Math.Pow(10, value);
                    break;
                case "log":
                    result = value <= 0 ? double.NaN : Math.Log10(value);
                    break;
                case "ln":
                    result = value <= 0 ? double.NaN : Math.Log(value);
                    break;
                case "n!":
                    if (value < 0 || value != Math.Floor(value))
                    {
                        entryResult.Text = "Geçersiz sayı!";
                        return;
                    }
                    result = 1;
                    for (int i = 1; i <= (int)value; i++)
                        result *= i;
                    break;
            }

            if (!error)
            {
                entryResult.Text = result.ToString("N");
                currentInput = result.ToString();
                isNewEntry = true;
            }
        }
    }
}
