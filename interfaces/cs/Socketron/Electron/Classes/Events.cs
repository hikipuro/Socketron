using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Event : JSObject {
		public Event() {
		}

		public void preventDefault() {
			API.Apply("preventDefault");
		}

		public WebContents sender {
			get { return API.GetObject<WebContents>("sender"); }
		}

		public JSObject returnValue {
			get { return API.GetObject<JSObject>("returnValue"); }
			set { API.SetObject("returnValue", value); }
		}

		public bool ctrlKey {
			get { return API.GetProperty<bool>("ctrlKey"); }
		}

		public bool metaKey {
			get { return API.GetProperty<bool>("metaKey"); }
		}

		public bool shiftKey {
			get { return API.GetProperty<bool>("shiftKey"); }
		}

		public bool altKey {
			get { return API.GetProperty<bool>("altKey"); }
		}
	}

	public class ConsoleMessageEvent : Event {
		public int level;
		public string message;
		public int line;
		public string sourceId;
	}

	public class DidChangeThemeColorEvent : Event {
		public string themeColor;
	}

	public class DidFailLoadEvent : Event {
		public int errorCode;
		public string errorDescription;
		public string validatedURL;
		public bool isMainFrame;
	}

	public class DidFrameFinishLoadEvent : Event {
		public bool isMainFrame;
	}

	public class DidGetRedirectRequestEvent : Event {
		public string oldURL;
		public string newURL;
		public bool isMainFrame;
	}

	public class DidGetResponseDetailsEvent : Event {
		public bool status;
		public string newURL;
		public string originalURL;
		public int httpResponseCode;
		public string requestMethod;
		public string referrer;
		public JsonObject headers;
		public string resourceType;
	}

	public class DidNavigateEvent : Event {
		public string url;
	}

	public class DidNavigateInPageEvent : Event {
		public bool isMainFrame;
		public string url;
	}

	public class FoundInPageResult {
		public int requestId;
		/// <summary>
		/// Position of the active match.
		/// </summary>
		public int activeMatchOrdinal;
		/// <summary>
		/// Number of Matches.
		/// </summary>
		public int matches;
		/// <summary>
		/// Coordinates of first match region.
		/// </summary>
		public JsonObject selectionArea;
		public bool finalUpdate;
	}

	public class FoundInPageEvent : Event {
		public FoundInPageResult result;
	}

	public class IpcMessageEvent : Event {
		public string channel;
		public object[] args;
	}

	public class LoadCommitEvent : Event {
		public string url;
		public bool isMainFrame;
	}

	public class NewWindowEvent : Event {
		public string url;
		public string frameName;
		public string disposition;
		public JsonObject options;
	}

	public class PageFaviconUpdatedEvent : Event {
		/// <summary>
		/// Array of URLs.
		/// </summary>
		public string[] favicons;
	}

	public class PageTitleUpdatedEvent : Event {
		public string title;
		public bool explicitSet;
	}

	public class PluginCrashedEvent : Event {
		public string name;
		public string version;
	}

	public class UpdateTargetUrlEvent : Event {
		public string url;
	}

	public class WillNavigateEvent : Event {
		public string url;
	}
}
