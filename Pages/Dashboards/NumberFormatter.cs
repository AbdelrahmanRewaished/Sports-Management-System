namespace Sports_Management_System.Pages.Dashboards
{
	public static class NumberFormatter
	{
		// 1234567 => 1,234,567
		public static string getFormattedNumber(int number)
		{
			return string.Format("{0:n0}", number);
		}
	}
}
