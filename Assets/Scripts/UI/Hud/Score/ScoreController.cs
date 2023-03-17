namespace Assets.Scripts.UI.Hud.Score
{
	public class ScoreController
	{
		private ScoreView _view;
		
		public ScoreController(ScoreView view)
		{
			_view = view;
			
			_view.SetScoreCount(0);
		}

		public void OnChangeScore(int value)
		{
			_view.SetScoreCount(value);
		}
	}
}