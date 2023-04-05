using Model.Background;
using UnityEngine;

namespace Views.GamePlay.Background
{
	public class BackgroundView: AbstractView, IBackgroundView
	{
		[SerializeField] private SpriteRenderer _back;
		
		private BackgroundModel Model => base.Model as BackgroundModel;

		protected override void SyncModel()
		{
			OnSizeChange(Model.BackgroundSize.Value);
			OnChangeParent(Model.Parent.Value);
		}
		
		protected override void AddChildListeners()
		{
			Model.BackgroundSize.Changed += OnSizeChange;
			Model.Parent.Changed += OnChangeParent;
		}

		protected override void RemoveChildListeners()
		{
			Model.BackgroundSize.Changed -= OnSizeChange;
			Model.Parent.Changed -= OnChangeParent;
		}

		public void OnSizeChange(Vector2 size)
		{
			_back.size = size;
		}
		
		public void OnChangeParent(Transform parent)
		{
			transform.SetParent(parent);
		}
	}
}