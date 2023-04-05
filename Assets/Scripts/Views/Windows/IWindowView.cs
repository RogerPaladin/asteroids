namespace Views.Windows
{
	internal interface IWindowView : IView
	{
		public void OnMinimize() { }
		public void OnMaximize() { }
		public void SetInteractiveState(bool interactive) { }
		public void OnHideStart() { }
		public void OnHideEnd() { }
	}
}