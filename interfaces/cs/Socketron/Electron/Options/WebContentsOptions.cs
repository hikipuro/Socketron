namespace Socketron.Electron {
	/// <summary>
	/// WebContents "found-in-page" event parameter.
	/// </summary>
	public class Result {
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

	/// <summary>
	/// WebContents "context-menu" event parameter.
	/// </summary>
	public class ContextMenuParams {
		/// <summary>
		/// x coordinate.
		/// </summary>
		public int? x;
		/// <summary>
		/// y coordinate.
		/// </summary>
		public int? y;
		/// <summary>
		/// URL of the link that encloses the node the context menu was invoked on.
		/// </summary>
		public string linkURL;
		/// <summary>
		/// Text associated with the link. May be an empty string
		/// if the contents of the link are an image.
		/// </summary>
		public string linkText;
		/// <summary>
		/// URL of the top level page that the context menu was invoked on.
		/// </summary>
		public string pageURL;
		/// <summary>
		/// URL of the subframe that the context menu was invoked on.
		/// </summary>
		public string frameURL;
		/// <summary>
		/// Source URL for the element that the context menu was invoked on.
		/// Elements with source URLs are images, audio and video.
		/// </summary>
		public string srcURL;
		/// <summary>
		/// Type of the node the context menu was invoked on.
		/// Can be none, image, audio, video, canvas, file or plugin.
		/// </summary>
		public string mediaType;
		/// <summary>
		/// Whether the context menu was invoked on an image which has non-empty contents.
		/// </summary>
		public bool? hasImageContents;
		/// <summary>
		/// Whether the context is editable.
		/// </summary>
		public bool? isEditable;
		/// <summary>
		/// Text of the selection that the context menu was invoked on.
		/// </summary>
		public string selectionText;
		/// <summary>
		/// Title or alt text of the selection that the context was invoked on.
		/// </summary>
		public string titleText;
		/// <summary>
		/// The misspelled word under the cursor, if any.
		/// </summary>
		public string misspelledWord;
		/// <summary>
		/// The character encoding of the frame on which the menu was invoked.
		/// </summary>
		public string frameCharset;
		/// <summary>
		/// If the context menu was invoked on an input field, the type of that field.
		/// Possible values are none, plainText, password, other.
		/// </summary>
		public string inputFieldType;
		/// <summary>
		/// Input source that invoked the context menu.
		/// Can be none, mouse, keyboard, touch, touchMenu.
		/// </summary>
		public string menuSourceType;
		/// <summary>
		/// The flags for the media element the context menu was invoked on.
		/// </summary>
		public MediaFlags mediaFlags;
		/// <summary>
		/// These flags indicate whether the renderer believes it is able to
		/// perform the corresponding action.
		/// </summary>
		public EditFlags editFlags;
	}

	/// <summary>
	/// WebContents "context-menu" event mediaFlags parameter.
	/// </summary>
	public class MediaFlags {
		/// <summary>
		/// Whether the media element has crashed.
		/// </summary>
		public bool? inError;
		/// <summary>
		/// Whether the media element is paused.
		/// </summary>
		public bool? isPaused;
		/// <summary>
		/// Whether the media element is muted.
		/// </summary>
		public bool? isMuted;
		/// <summary>
		/// Whether the media element has audio.
		/// </summary>
		public bool? hasAudio;
		/// <summary>
		/// Whether the media element is looping.
		/// </summary>
		public bool? isLooping;
		/// <summary>
		/// Whether the media element's controls are visible.
		/// </summary>
		public bool? isControlsVisible;
		/// <summary>
		/// Whether the media element's controls are toggleable.
		/// </summary>
		public bool? canToggleControls;
		/// <summary>
		/// Whether the media element can be rotated.
		/// </summary>
		public bool? canRotate;
	}

	/// <summary>
	/// WebContents "context-menu" event editFlags parameter.
	/// </summary>
	public class EditFlags {
		/// <summary>
		/// Whether the renderer believes it can undo.
		/// </summary>
		public bool? canUndo;
		/// <summary>
		/// Whether the renderer believes it can redo.
		/// </summary>
		public bool? canRedo;
		/// <summary>
		/// Whether the renderer believes it can cut.
		/// </summary>
		public bool? canCut;
		/// <summary>
		/// Whether the renderer believes it can copy.
		/// </summary>
		public bool? canCopy;
		/// <summary>
		/// Whether the renderer believes it can paste.
		/// </summary>
		public bool? canPaste;
		/// <summary>
		/// Whether the renderer believes it can delete.
		/// </summary>
		public bool? canDelete;
		/// <summary>
		/// Whether the renderer believes it can select all.
		/// </summary>
		public bool? canSelectAll;
	}

	/// <summary>
	/// WebContents.openDevTools() options.
	/// </summary>
	public class OpenDevToolsOptions {
		/// <summary>
		/// Opens the devtools with specified dock state,
		/// can be right, bottom, undocked, detach.
		/// Defaults to last used dock state.
		/// In undocked mode it's possible to dock back.In detach mode it's not.
		/// </summary>
		public string mode;

		/// <summary>
		/// mode values.
		/// </summary>
		public class Mode {
			public const string Right = "right";
			public const string Bottom = "bottom";
			public const string Undocked = "undocked";
			public const string Detach = "detach";
		}
	}

	/// <summary>
	/// WebContents.enableDeviceEmulation() parameters.
	/// </summary>
	public class Parameters {
		/// <summary>
		/// Specify the screen type to emulate (default: desktop)
		/// </summary>
		public string screenPosition;
		/// <summary>
		/// Set the emulated screen size (screenPosition == mobile)
		/// </summary>
		public Size screenSize;
		/// <summary>
		/// Position the view on the screen (screenPosition == mobile) (default: {x: 0, y: 0})
		/// </summary>
		public Point viewPosition;
		/// <summary>
		/// Set the device scale factor (if zero defaults to original device scale factor) (default: 0)
		/// </summary>
		public double deviceScaleFactor;
		/// <summary>
		/// Set the emulated view size (empty means no override)
		/// </summary>
		public Size viewSize;
		/// <summary>
		/// Whether emulated view should be scaled down if necessary to fit into available space (default: false)
		/// </summary>
		public bool fitToView;
		/// <summary>
		/// Offset of the emulated view inside available space (not in fit to view mode) (default: {x: 0, y: 0})
		/// </summary>
		public Point offset;
		/// <summary>
		/// Scale of emulated view inside available space (not in fit to view mode) (default: 1)
		/// </summary>
		public double scale;

		/// <summary>
		/// screenPosition values.
		/// </summary>
		public class ScreenPosition {
			public const string Desktop = "desktop";
			public const string Mobile = "mobile";
		}
	}

	/// <summary>
	/// WebContents.print() options.
	/// </summary>
	public class PrintOptions {
		/// <summary>
		/// Don't ask user for print settings. Default is false.
		/// </summary>
		public bool? silent;
		/// <summary>
		/// Also prints the background color and image of the web page. Default is false.
		/// </summary>
		public bool? printBackground;
		/// <summary>
		/// Set the printer device name to use. Default is ''.
		/// </summary>
		public string deviceName;
	}

	/// <summary>
	/// WebContents.printToPDF() options.
	/// </summary>
	public class PrintToPDFOptions {
		/// <summary>
		/// Specifies the type of margins to use.
		/// Uses 0 for default margin, 1 for no margin, and 2 for minimum margin.
		/// </summary>
		public int? marginsType;
		/// <summary>
		/// Specify page size of the generated PDF.
		/// Can be A3, A4, A5, Legal, Letter, Tabloid
		/// or an Object containing height and width in microns.
		/// </summary>
		public string pageSize;
		/// <summary>
		/// Whether to print CSS backgrounds.
		/// </summary>
		public bool? printBackground;
		/// <summary>
		/// Whether to print selection only.
		/// </summary>
		public bool? printSelectionOnly;
		/// <summary>
		/// true for landscape, false for portrait.
		/// </summary>
		public bool? landscape;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static PrintToPDFOptions Parse(string text) {
			return JSON.Parse<PrintToPDFOptions>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}

	/// <summary>
	/// SizeOptions normal.
	/// </summary>
	public class Normal {
		public int width;
		public int height;
	}

	/// <summary>
	/// WebContents.setSize() options.
	/// </summary>
	public class SizeOptions {
		/// <summary>
		/// Normal size of the page.
		/// This can be used in combination with the attribute to
		/// manually resize the webview guest contents.
		/// </summary>
		public Normal normal;
	}

	/// <summary>
	/// WebContents.findInPage() options.
	/// </summary>
	public class FindInPageOptions {
		/// <summary>
		/// Whether to search forward or backward, defaults to true.
		/// </summary>
		public bool? forward;
		/// <summary>
		/// Whether the operation is first request or a follow up, defaults to false.
		/// </summary>
		public bool? findNext;
		/// <summary>
		/// Whether search should be case-sensitive, defaults to false.
		/// </summary>
		public bool? matchCase;
		/// <summary>
		/// Whether to look only at the start of words. defaults to false.
		/// </summary>
		public bool? wordStart;
		/// <summary>
		/// When combined with wordStart, accepts a match in the middle of a word
		/// if the match begins with an uppercase letter followed by a lowercase
		/// or non-letter.Accepts several other intra-word matches, defaults to false.
		/// </summary>
		public bool? medialCapitalAsWordStart;
	}

	/// <summary>
	/// WebContents "before-input-event" event parameter.
	/// </summary>
	public class Input {
		/// <summary>
		/// Either keyUp or keyDown
		/// </summary>
		public string type;
		public string key;
		public string code;
		public bool isAutoRepeat;
		public bool shift;
		public bool control;
		public bool alt;
		public bool meta;
	}

	/// <summary>
	/// WebContents.startDrag() options.
	/// </summary>
	public class Item {
		/// <summary>
		/// or files Array The path(s) to the file(s) being dragged.
		/// </summary>
		public string file;
		/// <summary>
		/// The image must be non-empty on macOS.
		/// </summary>
		public NativeImage icon;
	}

	/// <summary>
	/// WebContents.loadURL() options.
	/// </summary>
	public class LoadURLOptions {
		/// <summary>
		/// A HTTP Referrer url.
		/// </summary>
		public string httpReferrer;
		/// <summary>
		/// A user agent originating the request.
		/// </summary>
		public string userAgent;
		/// <summary>
		/// Extra headers separated by "\n"
		/// </summary>
		public string extraHeaders;
		public object postData;
		/// <summary>
		/// Base url (with trailing path separator) for files to be loaded by the data url.
		/// This is needed only if the specified url is a data url and needs to load other files.
		/// </summary>
		public string baseURLForDataURL;
	}
}
