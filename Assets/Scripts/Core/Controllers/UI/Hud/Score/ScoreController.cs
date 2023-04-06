using Model.Score;
using Utils.DiContainers;

namespace Controllers.UI.Hud.Score
{
	public class ScoreController
	{
		public ScoreModel Model { get; private set; }

		public ScoreController(ScoreModel model)
		{
			Model = model;

			Model.SetScoreCount(0);
		}

		public void OnChangeScore(int value)
		{
			Model.SetScoreCount(value);
		}
	}
}