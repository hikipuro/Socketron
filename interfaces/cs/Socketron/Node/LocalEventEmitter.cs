using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	public delegate void EventListener(params object[] args);
	class EventListeners {
		public List<EventListener> _listeners;
		public Dictionary<EventListener, bool> _isOnce;

		public EventListeners() {
			_listeners = new List<EventListener>();
			_isOnce = new Dictionary<EventListener, bool>();
		}

		public EventListener this[int i] {
			get { return _listeners[i]; }
		}

		public int Count {
			get { return _listeners.Count; }
		}

		public void On(EventListener listener) {
			_listeners.Add(listener);
			_isOnce.Add(listener, false);
		}

		public void Once(EventListener listener) {
			_listeners.Add(listener);
			_isOnce.Add(listener, true);
		}

		public void Remove(EventListener listener) {
			_listeners.Remove(listener);
			_isOnce.Remove(listener);
		}

		public void RemoveList(List<int> indices) {
			foreach (int i in indices) {
				EventListener listener = _listeners[i];
				_listeners.RemoveAt(i);
				_isOnce.Remove(listener);
			}
		}

		public bool IsOnce(EventListener listener) {
			if (!_isOnce.ContainsKey(listener)) {
				return true;
			}
			return _isOnce[listener];
		}
	}

	public class LocalEventEmitter {
		private Dictionary<string, EventListeners> _listeners;

		public LocalEventEmitter() {
			_listeners = new Dictionary<string, EventListeners>();
		}

		public void Emit(string channel, params object[] args) {
			channel = channel.ToLower();
			if (!_listeners.ContainsKey(channel)) {
				return;
			}

			List<int> removeList = new List<int>();
			EventListeners listeners = _listeners[channel];
			for (int i = 0; i < listeners.Count; i++) {
				EventListener listener = listeners[i];
				listener?.Invoke(args);
				if (listeners.IsOnce(listener)) {
					removeList.Add(i);
				}
			}
			removeList.Reverse();
			listeners.RemoveList(removeList);
			if (_listeners[channel].Count <= 0) {
				_listeners.Remove(channel);
			}
		}

		public void EmitNewThread(string channel, params object[] args) {
			/*
			WaitCallback callback = new WaitCallback((state) => {
				Thread.CurrentThread.Name = "EventEmitter.EmitNewThread: " + channel;
				Emit(channel, args);
			});
			ThreadPool.QueueUserWorkItem(callback);
			//*/
			Thread thread = new Thread(() => {
				Emit(channel, args);
			});
			thread.Name = "EventEmitter.EmitNewThread: " + channel;
			thread.Start();
		}

		/*
		public async void EmitTask(string channel, params object[] args) {
			Task task = Task.Run(() => {
				Emit(channel, args);
			});
			await task;
		}
		//*/

		public LocalEventEmitter On(string channel, EventListener listener) {
			channel = channel.ToLower();
			if (!_listeners.ContainsKey(channel)) {
				_listeners.Add(channel, new EventListeners());
			}
			_listeners[channel].On(listener);
			return this;
		}

		public LocalEventEmitter Once(string channel, EventListener listener) {
			channel = channel.ToLower();
			if (!_listeners.ContainsKey(channel)) {
				_listeners.Add(channel, new EventListeners());
			}
			_listeners[channel].Once(listener);
			return this;
		}

		public void RemoveAllListeners() {
			_listeners.Clear();
		}

		public void RemoveAllListeners(string channel) {
			channel = channel.ToLower();
			if (_listeners.ContainsKey(channel)) {
				_listeners.Remove(channel);
			}
		}

		public void RemoveListener(string channel, EventListener listener) {
			channel = channel.ToLower();
			if (_listeners.ContainsKey(channel)) {
				_listeners[channel].Remove(listener);
			}
			if (_listeners[channel].Count <= 0) {
				_listeners.Remove(channel);
			}
		}

		public string[] EventNames() {
			string[] keys = new string[_listeners.Keys.Count];
			_listeners.Keys.CopyTo(keys, 0);
			return keys;
		}

		public int ListenerCount(string channel) {
			channel = channel.ToLower();
			if (!_listeners.ContainsKey(channel)) {
				return 0;
			}
			return _listeners[channel].Count;
		}
	}
}
