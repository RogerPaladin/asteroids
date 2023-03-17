using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Hud.PlayerInfo
{
	public class PlayerInfoView: MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _coords;
		[SerializeField] private TextMeshProUGUI _angle;
		[SerializeField] private TextMeshProUGUI _speed;
		
		public void SetCoords(Vector2 coords)
		{
			_coords.text = $"{Math.Round(coords.x)}; {Math.Round(coords.y)}";
		}
		
		public void SetAngle(float angle)
		{
			_angle.text = Math.Round(angle).ToString();
		}
		
		public void SetSpeed(float speed)
		{
			_speed.text = speed.ToString("F2");
		}
	}
}