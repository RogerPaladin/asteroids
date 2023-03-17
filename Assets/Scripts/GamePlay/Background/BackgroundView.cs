using UnityEngine;

namespace Assets.Scripts.GamePlay.Background
{
	public class BackgroundView: MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _back;

		public void Watch(BackgroundModel model)
		{
			model.OnSizeChangeEvent += OnSizeChange;
		}

		public void UnWatch(BackgroundModel model)
		{
			model.OnSizeChangeEvent -= OnSizeChange;
		}
		
		private void OnSizeChange(Vector2 size)
		{
			_back.size = size;
		}
	}
}