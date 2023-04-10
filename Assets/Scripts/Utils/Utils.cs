using System;

namespace Utils
{
	public static class Utils
	{
		//todo!!!! Утилиты можно схлопнуть, а метод GetNumericTime упростить. + если появятся часы-минуты, лучше писать новые методы, а не делать универсальный. Этот выглядит жутко
		public static string GetNumericTime(this float time, bool needHours = true, bool needSeconds = true)
		{
			var hours = (int) Math.Floor(time / 3600f);
			var minutes = (int) Math.Floor((time - hours*3600f) / 60f);
			var seconds = (int) time - hours* 3600 - minutes*60;

			if (!needSeconds && hours == 0 && minutes == 0 && seconds > 0)
			{
				minutes += 1;
			}

			if (needHours || hours > 0)
			{
				return fillDigits(hours) + ":" + fillDigits(minutes) + (needSeconds? ":" + fillDigits(seconds) : "");
			}
			else
			{
				return fillDigits(minutes) + ":" + (needSeconds? fillDigits(seconds) : "");
			}

			string fillDigits(int val)
			{
				string result = "";

				if (val == 0)
					return "00";

				result = val.ToString();
				while (result.Length< 2)
				{
					result = "0" + result;
				}

				return result;
			}
		}
	}
}