using System;
using System.Collections.Generic;

namespace Socketron {
	public class EventEmitter {
		protected Dictionary<string, List<Action<object[]>>> _events;
		protected Dictionary<string, List<Action<object[]>>> _eventsOnce;

		public EventEmitter() {
			_events = new Dictionary<string, List<Action<object[]>>>();
			_eventsOnce = new Dictionary<string, List<Action<object[]>>>();
		}

		public void Emit(string channel, params object[] args) {
			channel = channel.ToLower();
			if (_events.ContainsKey(channel)) {
				foreach (Action<object[]> func in _events[channel]) {
					func(args);
				}
			}
			if (_eventsOnce.ContainsKey(channel)) {
				foreach (Action<object[]> func in _eventsOnce[channel]) {
					func(args);
				}
				_eventsOnce[channel].Clear();
			}
		}

		public EventEmitter On(string channel, Action<object[]> listener) {
			channel = channel.ToLower();
			if (!_events.ContainsKey(channel)) {
				_events.Add(channel, new List<Action<object[]>>());
			}
			_events[channel].Add(listener);
			return this;
		}

		public EventEmitter Once(string channel, Action<object[]> listener) {
			channel = channel.ToLower();
			if (!_eventsOnce.ContainsKey(channel)) {
				_eventsOnce.Add(channel, new List<Action<object[]>>());
			}
			_eventsOnce[channel].Add(listener);
			return this;
		}

		public void RemoveAllListeners() {
			_events.Clear();
		}

		public void RemoveAllListeners(string channel) {
			channel = channel.ToLower();
			if (!_events.ContainsKey(channel)) {
				return;
			}
			_events.Remove(channel);
		}

		public void RemoveListener(string channel, Action<object[]> listener) {
			channel = channel.ToLower();
			if (!_events.ContainsKey(channel)) {
				return;
			}
			_events[channel].Remove(listener);
		}
	}
}
