using Utils.Reactivity;

namespace Model.Score
{
	public class ScoreModel : IModel
	{
		public Observable<int> Score { get; } = new Observable<int>(0);

		public void SetScoreCount(int score)
		{
			Score.Value = score;
		}
	}
}