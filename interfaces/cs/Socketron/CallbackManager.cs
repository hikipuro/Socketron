using System.Collections.Generic;

namespace Socketron {
	using KeyType = System.Int32;
	using CallbackType = JSCallback;

	public delegate void JSCallback(object[] result);

	public class CallbackItem {
		public CallbackType Callback;
		public KeyType CallbackId;
		public KeyType ObjectId;

		public CallbackItem(CallbackType callback, KeyType callbackId) {
			Callback = callback;
			CallbackId = callbackId;
		}
	}

	public class CallbackList {
		protected KeyType _nextId;
		protected Dictionary<KeyType, CallbackItem> _items;
		protected Dictionary<CallbackType, KeyType> _index;

		public CallbackList() {
			_nextId = 1;
			_items = new Dictionary<KeyType, CallbackItem>();
			_index = new Dictionary<CallbackType, KeyType>();
		}

		public CallbackItem this[KeyType key] {
			get { return _items[key]; }
		}

		public CallbackItem Add(CallbackType callback) {
			KeyType currentId = _nextId++;
			CallbackItem item = new CallbackItem(callback, currentId);
			_items.Add(currentId, item);
			_index.Add(callback, currentId);
			return item;
		}

		public bool Remove(KeyType id) {
			if (!_items.ContainsKey(id)) {
				return false;
			}
			CallbackItem callback = _items[id];
			_index.Remove(callback.Callback);
			return _items.Remove(id);
		}

		public bool Remove(CallbackType callback) {
			if (!_index.ContainsKey(callback)) {
				return false;
			}
			return Remove(_index[callback]);
		}

		public void RemoveAll() {
			_items.Clear();
			_index.Clear();
		}

		public KeyType GetKey(CallbackType callback) {
			if (!_index.ContainsKey(callback)) {
				return -1;
			}
			return _index[callback];
		}

		public CallbackItem GetItem(CallbackType callback) {
			if (!_index.ContainsKey(callback)) {
				return null;
			}
			return _items[_index[callback]];
		}

		public bool ContainsKey(KeyType id) {
			return _items.ContainsKey(id);
		}

		public bool ContainsValue(CallbackType callback) {
			return _index.ContainsKey(callback);
		}
	}

	public class InstanceCallbacks : Dictionary<string, CallbackList> {
	}

	public class CallbackManager {
		protected Dictionary<KeyType, InstanceCallbacks> _classes;

		public CallbackManager() {
			_classes = new Dictionary<KeyType, InstanceCallbacks>();
		}

		public CallbackItem Add(KeyType instanceId, string eventName, CallbackType callback) {
			if (!_classes.ContainsKey(instanceId)) {
				_classes.Add(instanceId, new InstanceCallbacks());
			}
			if (!_classes[instanceId].ContainsKey(eventName)) {
				_classes[instanceId].Add(eventName, new CallbackList());
			}
			return _classes[instanceId][eventName].Add(callback);
		}

		public bool RemoveItem(KeyType instanceId, string eventName, KeyType id) {
			if (!_IsValidKey(instanceId, eventName)) {
				return false;
			}
			return _classes[instanceId][eventName].Remove(id);
		}

		/*
		public bool Remove(long instanceId, string eventName, Callback callback) {
			if (!_IsValidKey(instanceId, eventName)) {
				return false;
			}
			return _classes[instanceId][eventName].Remove(callback);
		}
		//*/

		public void RemoveInstanceEvents(KeyType instanceId) {
			if (!_classes.ContainsKey(instanceId)) {
				return;
			}
			_classes[instanceId].Clear();
			_classes.Remove(instanceId);
		}

		public void RemoveEvents(KeyType instanceId, string eventName) {
			if (!_IsValidKey(instanceId, eventName)) {
				return;
			}
			_classes[instanceId][eventName].RemoveAll();
			_classes[instanceId].Remove(eventName);
		}

		/*
		public KeyType GetId(string className, string eventName, Callback callback) {
			if (!_IsValidKey(className, eventName)) {
				return -1;
			}
			return _classes[className][eventName].GetKey(callback);
		}
		//*/

		public CallbackItem GetItem(KeyType instanceId, string eventName, KeyType id) {
			if (!_IsValidKey(instanceId, eventName)) {
				return null;
			}
			if (!_classes[instanceId][eventName].ContainsKey(id)) {
				return null;
			}
			return _classes[instanceId][eventName][id];
		}

		public CallbackItem GetItem(KeyType instanceId, string eventName, CallbackType callback) {
			if (!_IsValidKey(instanceId, eventName)) {
				return null;
			}
			if (!_classes[instanceId][eventName].ContainsValue(callback)) {
				return null;
			}
			return _classes[instanceId][eventName].GetItem(callback);
		}

		/*
		public void SetObjectId(string className, string eventName, KeyType id, long objectId) {
			if (!_IsValidName(className, eventName)) {
				return;
			}
			if (!_classes[className][eventName].ContainsKey(id)) {
				return;
			}
			_classes[className][eventName][id].ObjectId = objectId;
		}
		//*/

		/*
		public KeyType GetObjectId(string className, string eventName, KeyType id) {
			if (!_IsValidName(className, eventName)) {
				return 0;
			}
			if (!_classes[className][eventName].ContainsKey(id)) {
				return 0;
			}
			return _classes[className][eventName][id].ObjectId;
		}
		//*/

		protected bool _IsValidKey(KeyType instanceId, string eventName) {
			if (!_classes.ContainsKey(instanceId)) {
				return false;
			}
			if (!_classes[instanceId].ContainsKey(eventName)) {
				return false;
			}
			return true;
		}
	}
}
