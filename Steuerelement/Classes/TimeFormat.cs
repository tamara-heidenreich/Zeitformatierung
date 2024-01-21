namespace Steuerelement.Classes
{
    /// <summary>
    /// Klasse zur Speicherung der relevanten Informationen zur Konvertierung.
    /// </summary>
    public class TimeFormat(string input, string output, string status, string precision)
    {
        public string Input { get; set; } = input;
        public string Output { get; set; } = output;
        public string Status { get; set; } = status;
        public string Precision { get; set; } = precision;
    }
}
