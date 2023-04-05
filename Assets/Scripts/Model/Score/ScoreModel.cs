using Utils.Reactivity;

namespace Model.Score
{
	public class ScoreModel : IModel
	{
		public Observable<int> Score { get; private set; } = new Observable<int>(0);

		public void SetScoreCount(int score)
		{
			Score.Value = score;
		}
	}
}