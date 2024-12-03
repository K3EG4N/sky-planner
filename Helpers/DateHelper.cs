namespace SkyPlanner.Helpers
{
    public class DateHelper
    {
        public string ConvertDateToString(DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }

            return date.Value.ToString("yyyy/MM/dd");
        }
    }
}
