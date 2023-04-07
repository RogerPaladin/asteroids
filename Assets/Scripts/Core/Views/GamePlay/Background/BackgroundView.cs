using Model.Background;
using UnityEngine;

namespace Views.GamePlay.Background
{
	public class BackgroundView: AbstractView<BackgroundModel>, IBackgroundView
	{
		[SerializeField] private SpriteRenderer _back;
		
		protected override void SyncModel()
		{
			OnSizeChange(Model.BackgroundSize.Value);
		}
		
		protected override void AddChildListeners()
		{
			Model.BackgroundSize.Changed += OnSizeChange;
		}

		protected override void RemoveChildListeners()
		{
			Model.BackgroundSize.Changed -= OnSizeChange;
		}

		public void OnSizeChange(Vector2 size)
		{
			_back.size = size;
		}
	}
}