namespace Steuerelement.Classes
{
    /// <summary>
    /// Klasse zum Speichern der User-Eingaben in einer Liste für die Ausgabe.
    /// </summary>
    public class TimeFormatManagement
    {
        private List<TimeFormat> TimeFormatList { get; set; }

        public TimeFormatManagement()
        {
            TimeFormatList = new List<TimeFormat>();
        }

        public void Add(string input, string output, string status, string precision)
        {
            TimeFormatList.Add(new TimeFormat(input, output, status, precision));
        }

        public List<TimeFormat> Get()
        {
            return TimeFormatList;
        }
    }
}
