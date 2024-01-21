using System.Text.RegularExpressions;

namespace Steuerelement.Classes
{
    /// <summary>
    /// Klasse für die Aufbereitung und Formatierung der User-Eingabe.
    /// </summary>
    public class FormatInput
    {

        public string input = "";
        public string status = "";
        public string precision = "";
        private bool isNegative = false;
        public TimeFormatManagement timeFormat = new();

        /* -- Daten und Ausgabe zum Testen --
        public string[] inputsList = { "02", "12345", "1:23.45", "1234*", "1:23.4*", "02", "0123", "0123*", "0001" };

        public FormatInput()
        {
            foreach(var item in inputsList) {
                handleInput(item, "1/10");
                handleInput(item, "1/100");
            }
        }
        */

        /// <summary>
        /// Methode zum Überprüfen der User-Eingabe auf ihre Zulässigkeit.
        /// </summary>
        /// <param name="input">User-Eingabe</param>
        /// <returns>true or false</returns>
        public bool checkInput(string input)
        {
            Regex regex = new Regex(@"[\d*,.:\-+]+");

            if (input.StartsWith("-"))
            {
                isNegative = true;
            }

            if ((regex.IsMatch(input) || String.IsNullOrEmpty(input)) &&
                cleanInput(input).Length <= 8)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Methode zum Entfernen von Sonderzeichen aus der User-Eingabe.
        /// </summary>
        /// <param name="input">User-Eingabe</param>
        /// <returns>Bereinigter String</returns>
        private string cleanInput(string input)
        {
            string result = input.Replace("-", "").Replace(".", "").Replace(":", "").Replace(",", "").Replace("+", "");
            while (result.StartsWith("0"))
            {
                result = result.Substring(1);
            }
            return result;
        }

        /// <summary>
        /// Methode zum Ermitteln des Modus für die Formatierung der User-Eingabe.
        /// </summary>
        /// <param name="input">User-Eingabe</param>
        /// <param name="precision">Genauigkeit</param>
        /// <returns>Modus als int</returns>
        private int GetMode(string input, string precision)
        {

            if (input.Contains("*") || precision.Equals("1/100"))
            {
                return 0;
            }
            else if (precision.Equals("1/10"))
            {
                return 1;
            }
            return -1;

        }

        /// <summary>
        /// Methode zum Formatieren der User-Eingabe gemäß der angegebenen Genauigkeit.
        /// </summary>
        /// <param name="input">User-Eingabe</param>
        /// <param name="precision">Genauigkeit</param>
        /// <returns>formatierter String</returns>
        public String ConvertInput(string input, string precision)
        {
            int mode = GetMode(input, precision);
            string reversedInput = new(input.Reverse().ToArray());
            if (reversedInput.Length < 3)
            {
                while (reversedInput.Length < 3)
                {
                    reversedInput += '0';
                }
            }
            string tmpoutput = "";
            for (int i = 0; i < reversedInput.Length; i++)
            {
                tmpoutput += reversedInput[i];
                if (mode == 0)
                {
                    if (i == 1)
                    {
                        tmpoutput += ".";
                    }
                    else if (i % 2 == 1 && i < reversedInput.Length - 1)
                    {
                        tmpoutput += ":";
                    }
                }
                else if (mode == 1)
                {
                    if (i == 0)
                    {
                        tmpoutput += ".";
                    }
                    else if (i % 2 == 0 && i < reversedInput.Length - 1)
                    {
                        tmpoutput += ":";
                    }
                }
            }
            string converted = new(tmpoutput.Reverse().ToArray());
            if (converted.StartsWith("00"))
            {
                converted = converted.Substring(1);
            }
            if (isNegative)
            {
                converted = "-" + converted;
                isNegative = false;
            }
            return converted;
        }

        /// <summary>
        /// Methode zur gesamten Verarbeitung der User-Eingabe.
        /// </summary>
        /// <param name="input">User-Eingabe</param>
        /// <param name="precision">Genauigkeit</param>

        public void handleInput(string input, string precision)
        {
            string output;
            if (checkInput(input))
            {
                input = cleanInput(input);
                output = ConvertInput(input, precision);
                timeFormat.Add(this.input, output, this.status, precision);

            }
            else
            {
                output = "Ungültige Eingabe!";
            }
        }

        /// <summary>
        /// Methode für das Frontend zum Aufrufen der Verarbeitungs-Methode.
        /// </summary>
        public void onClickFormat()
        {
            handleInput(input, precision);
        }

    }
}
