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

		public Gamepad[] getGamepads() {
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
			object[] result = API.ExecuteCachedScript<object[]>(cacheId2);
			if (result == null) {
				return new Gamepad[0];
			}
			int[] ids = Array.ConvertAll(result, value => Convert.ToInt32(value));
			Gamepad[] gamepads = new Gamepad[ids.Length];
			for (int i = 0; i < ids.Length; i++) {
				int id = ids[i];
				if (id <= 0) {
					continue;
				}
				gamepads[i] = API.CreateObject<Gamepad>(id);
			}
			return gamepads;
		}
	}
}
