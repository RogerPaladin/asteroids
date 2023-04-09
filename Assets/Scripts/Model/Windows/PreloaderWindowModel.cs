using Static.Windows;
using Utils.Reactivity;

namespace Model.Windows
{
	public class PreloaderWindowModel : AbstractWindowModel
	{
		public Observable<int> Progress { get; private set; } = new Observable<int>(0);
		
		public Observable<bool> NeedShowProgressBar { get; private set; } = new Observable<bool>(true);
		public Observable<bool> NeedShowAnyKeyText { get; private set; } = new Observable<bool>(false);

		public override WindowType Type => WindowType.Preloader;
		
		public void SetProgress(int progress)
		{
			Progress.Value = progress;
		}

		public void OnCompleteLoad()
		{
			NeedShowProgressBar.Value = false;
			NeedShowAnyKeyText.Value = true;
		}


	}
}