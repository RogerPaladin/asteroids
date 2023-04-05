using System;
using System.Collections.Generic;
using System.Linq;
using Utils.DiContainers;
using Views.Windows;
using UnityEngine;
using Utils.Containers.UI.Windows;
using Object = UnityEngine.Object;

namespace Controllers.UI.WindowsSystem
{
	public class WindowsController : IDiContainerChild
	{
		private readonly WindowsContainer _windowsContainer;
		
		private AbstractWindow _currentWindow = null;

		private Stack<AbstractWindow> _openWindowsStack { get; set; } = new Stack<AbstractWindow>(10);
		private Queue<(AbstractWindow, Action<AbstractWindow>)> _windowsQueue { get; } = new Queue<(AbstractWindow, Action<AbstractWindow>)>(10);

		public WindowsController(WindowsContainer windowsContainer)
		{
			_windowsContainer = windowsContainer;
		}

		protected T OpenWindow<T>(Action<T> action) where T : AbstractWindow
		{
			var window = GetWindow<T>();

			if (window == null)
			{
				Debug.LogError($"Window '{typeof(T).Name}' not found");
				return null;
			}

			_openWindowsStack.Push(window);

			_currentWindow = window;
			action?.Invoke(window);

			return window;
		}

		private T GetWindow<T>() where T : AbstractWindow
		{
			Type type = typeof(T);

			var prefabName = type.Name + "View";

			var windowView = Resources.Load<AbstractWindowView>($"Windows/{prefabName}");
							
			if (windowView)
			{
				var viewInstance = Object.Instantiate(windowView, _windowsContainer.transform);
				T controller = (T)Activator.CreateInstance(type, this, null);
				viewInstance.BindModel(controller.Model);
				return controller;
			}
			
			throw new Exception($"Window not found {prefabName}!");
		}
		
		public T ShowWindow<T>(bool closeAllOther = false, Action<T> action = null, bool minimizeTopScreen = true) where T : AbstractWindow
		{
			if (closeAllOther)
			{
				if (_currentWindow != null && !_currentWindow.Model.CanClose.Value)
					return null;
				
				CloseAllScreens($"by {typeof(T)}");
			}
			else if (minimizeTopScreen)
				MinimizeTopScreen();

			return OpenWindow(action);
		}

		public void CloseCommand()
		{
			CloseTopScreen();
		}
		
		public void CloseAllScreens(string tag)
		{
			Debug.Log("CloseAllScreens " + (tag ?? ""));

			while (_openWindowsStack.Count > 0)
			{
				var wnd = _openWindowsStack.Pop();
				if (wnd != null)
				{
					wnd.Close();
				}
			}
			
			_currentWindow = null;
		}

		protected void ClearEmptyScreensIfAny()
		{
			if (_openWindowsStack.Count <= 0) return;
			AbstractWindow wnd = _openWindowsStack.Peek();

			while (wnd == null)
			{
				_openWindowsStack.Pop();
				if (_openWindowsStack.Count <= 0) return;
				wnd = _openWindowsStack.Peek();
			}
		}
		
		private void CloseTopScreen()
		{
			ClearEmptyScreensIfAny();

			if (_openWindowsStack.Count <= 0) return;

			var win = _openWindowsStack.Pop();
			if (win.Model.CanClose.Value)
				win.Close();

			OnTopScreenClose();
		}

		protected void OnTopScreenClose()
		{
			ClearEmptyScreensIfAny();
			if (_openWindowsStack.Count > 0)
			{
				_openWindowsStack.Peek().Maximize();
				_currentWindow = _openWindowsStack.Peek();
			}
			else
			{
				_currentWindow = null;
				DisplayWindowInQueueIfAny();
			}
		}
		
		protected void MinimizeTopScreen()
		{
			ClearEmptyScreensIfAny();

			if (_openWindowsStack.Count > 0)
			{
				_openWindowsStack.Peek().Minimize();
			}
		}
		
		public void RemoveFromController(AbstractWindow wnd)
		{
			if (wnd == null)
				return;

			ClearEmptyScreensIfAny();

			if (!_openWindowsStack.Contains(wnd))
				return;
			
			var topScreen = false;

			if (_openWindowsStack.Count > 0)
				topScreen = _openWindowsStack.Peek() == wnd;
			
			_openWindowsStack = new Stack<AbstractWindow>(_openWindowsStack
														 .ToArray()
														 .Where(x => x != wnd)
														 .Reverse());

			if (_openWindowsStack.Count == 0)
			{
				_currentWindow = null;
			}

			if (topScreen)
				OnTopScreenClose();
		}
		
		protected bool DisplayWindowInQueueIfAny()
		{
			if (_windowsQueue.Count <= 0) return false;
			if (_currentWindow == null) return false;

			var (window, action) = _windowsQueue.Dequeue();
			while (window == null && _windowsQueue.Count > 0)
			{
				(window, action) = _windowsQueue.Dequeue();
			}
			if (window == null) return false;

			OpenWindow(action);
			return true;
		}

	}
}