using System;
using System.Collections.Generic;

namespace Socketron {
	class EventListeners {
		public List<Action<object[]>> _listeners;
		public Dictionary<Action<object[]>, bool> _isOnce;

		public EventListeners() {
			_listeners = new List<Action<object[]>>();
			_isOnce = new Dictionary<Action<object[]>, bool>();
		}

		public Action<object[]> this[int i] {
			get { return _listeners[i]; }
		}

		public int Count {
			get { return _listeners.Count; }
		}

		public void On(Action<object[]> listener) {
			_listeners.Add(listener);
			_isOnce.Add(listener, false);
		}

		public void Once(Action<object[]> listener) {
			_listeners.Add(listener);
			_isOnce.Add(listener, true);
		}

		public void Remove(Action<object[]> listener) {
			_listeners.Remove(listener);
			_isOnce.Remove(listener);
		}

		public void RemoveList(List<int> indices) {
			foreach (int i in indices) {
				Action<object[]> listener = _listeners[i];
				_listeners.RemoveAt(i);
				_isOnce.Remove(listener);
			}
		}

		public bool IsOnce(Action<object[]> listener) {
			if (!_isOnce.ContainsKey(listener)) {
				return true;
			}
			return _isOnce[listener];
		}
	}

	public class EventEmitter {
		private Dictionary<string, EventListeners> _listeners;

		public EventEmitter() {
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
				Action<object[]> listener = listeners[i];
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

		public EventEmitter On(string channel, Action<object[]> listener) {
			channel = channel.ToLower();
			if (!_listeners.ContainsKey(channel)) {
				_listeners.Add(channel, new EventListeners());
			}
			_listeners[channel].On(listener);
			return this;
		}

		public EventEmitter Once(string channel, Action<object[]> listener) {
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

		public void RemoveListener(string channel, Action<object[]> listener) {
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
