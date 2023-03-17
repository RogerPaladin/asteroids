using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Utils
{
	public static class Utils
	{
		public static string GetResourcesStaticPath(string filename) => "Static/" + filename;
		
		public static T LoadJsonFromResources<T>(string fileName)
		{
			var fileInResources = Resources.Load<TextAsset>(GetResourcesStaticPath(fileName));
			if (!fileInResources)
				return default;

			if (!string.IsNullOrEmpty(fileInResources.text))
			{
				try
				{
					return JsonConvert.DeserializeObject<T>(fileInResources.text);
				}
				catch (Exception e)
				{
					Debug.LogError($"Bad file {GetResourcesStaticPath(fileName)}\n{e.Message}");
					return default;
				}
			}

			return default;
		}
		
		public static List<T> GetAllPublicStaticFieldsValues<T>(this Type type)
		{
			return type
				  .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
				  .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
				  .Select(x => (T)x.GetRawConstantValue())
				  .ToList();
		}

		public static Vector2 GetRandomPosition(Camera camera, Vector2 exceptPosition, float minDistance = 5)
		{
			var center = Vector2.zero;
			var randomPoint = camera.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

			var dist = (center - (Vector2)randomPoint).magnitude;

			if (dist < minDistance)
				return GetRandomPosition(camera, exceptPosition, minDistance);

			return randomPoint;
		}
		
		public static string GetNumericTime(this float time, bool needHours = true, bool needSeconds = true)
		{
			var hours = (int) Math.Floor(time / 3600f);
			var minutes = (int) Math.Floor((time - hours*3600f) / 60f);
			var seconds = (int) time - hours* 3600 - minutes*60;

			if (!needSeconds && hours == 0 && minutes == 0 && seconds > 0)
			{
				minutes += 1;
			}

			if (needHours || hours > 0)
			{
				return fillDigits(hours) + ":" + fillDigits(minutes) + (needSeconds? ":" + fillDigits(seconds) : "");
			}
			else
			{
				return fillDigits(minutes) + ":" + (needSeconds? fillDigits(seconds) : "");
			}

			string fillDigits(int val)
			{
				string result = "";

				if (val == 0)
					return "00";

				result = val.ToString();
				while (result.Length< 2)
				{
					result = "0" + result;
				}

				return result;
			}
		}
	}
}