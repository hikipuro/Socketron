using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Navigator : DOMModule {
		Dictionary<int, int> _getGamepadsCache = new Dictionary<int, int>();

		public Navigator() {
		}

		public string userAgent {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.userAgent;",
					Script.GetObject(API.id)
				);
				return API._ExecuteBlocking<string>(script);
			}
		}

		public List<Gamepad> getGamepads() {
			if (!_getGamepadsCache.ContainsKey((API as SocketronDOMAPI).webContentsId)) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var result = [];",
						"var array = {0}.getGamepads();",
						"for (var item of array) {{",
							"if (item == null) {{",
								"result.push(0)",
								"continue;",
							"}}",
							"result.push({1});",
						"}}",
						"return result;"
					),
					Script.GetObject(API.id),
					Script.AddObject("item")
				);
				int cacheId = API.CacheScript(script);
				_getGamepadsCache.Add((API as SocketronDOMAPI).webContentsId, cacheId);
			}
			int cacheId2 = _getGamepadsCache[(API as SocketronDOMAPI).webContentsId];
			List<Gamepad> gamepads = new List<Gamepad>();
			object[] result = API.ExecuteCachedScript<object[]>(cacheId2);
			if (result == null) {
				return gamepads;
			}
			int[] ids = Array.ConvertAll(result, value => Convert.ToInt32(value));
			foreach (int id in ids) {
				if (id <= 0) {
					gamepads.Add(null);
					continue;
				}
				gamepads.Add(API.CreateObject<Gamepad>(id));
			}
			return gamepads;
		}
	}
}
