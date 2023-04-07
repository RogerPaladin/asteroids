using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using Views;

namespace Controllers.UI.Windows
{
	public class WindowsSystem
	{
		private readonly Transform _windowsContainer;

		private AbstractWindow _currentWindow = null;

		private Stack<AbstractWindow> _openWindowsStack { get; set; } = new Stack<AbstractWindow>(10);

		public WindowsSystem(Transform windowsContainer)
		{
			_windowsContainer = windowsContainer;
		}

		private T GetWindow<T>(IModel model) where T : AbstractWindow
		{
			Type type = typeof(T);

			T controller = (T)Activator.CreateInstance(type, this, model);
			return controller;
		}
		
		public T ShowWindow<T>(IModel model, IView view, bool closeAllOther = false, bool minimizeTopScreen = true) where T : AbstractWindow
		{
			if (closeAllOther)
			{
				if (_currentWindow != null)
					return null;
				
				CloseAllScreens($"by {typeof(T)}");
			}
			else if (minimizeTopScreen)
				MinimizeTopScreen();
			
			var window = GetWindow<T>(model);

			view.SetParent(_windowsContainer, false);

			_openWindowsStack.Push(window);

			_currentWindow = window;

			return window;
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
	}
}