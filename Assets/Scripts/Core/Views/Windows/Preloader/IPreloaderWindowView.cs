namespace Views.Windows.Preloader
{
	public interface IPreloaderWindowView
	{
		void SetProgress(int val);
		void HideProgressBar();
		void ShowPressAnyKeyText();
	}
}