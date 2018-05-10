using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Node : EventTarget {
		public class NodeTypes {
			public const int ELEMENT_NODE = 1;
			public const int ATTRIBUTE_NODE = 2;
			public const int TEXT_NODE = 3;
			public const int CDATA_SECTION_NODE = 4;
			public const int ENTITY_REFERENCE_NODE = 5;
			public const int ENTITY_NODE = 6;
			public const int PROCESSING_INSTRUCTION_NODE = 7;
			public const int COMMENT_NODE = 8;
			public const int DOCUMENT_NODE = 9;
			public const int DOCUMENT_TYPE_NODE = 10;
			public const int DOCUMENT_FRAGMENT_NODE = 11;
			public const int NOTATION_NODE = 12;
		}

		public class DocumentPositions {
			public const int DOCUMENT_POSITION_DISCONNECTED = 1;
			public const int DOCUMENT_POSITION_PRECEDING = 2;
			public const int DOCUMENT_POSITION_FOLLOWING = 4;
			public const int DOCUMENT_POSITION_CONTAINS = 8;
			public const int DOCUMENT_POSITION_CONTAINED_BY = 16;
			public const int DOCUMENT_POSITION_IMPLEMENTATION_SPECIFIC = 32;
		}

		public Node() {
		}

		public string baseURI {
			get { return API.GetProperty<string>("baseURI"); }
		}

		/*
		public string childNodes {
			get { return API.GetProperty<string>("childNodes"); }
		}
		//*/

		public Node firstChild {
			get { return API.GetObject<Node>("firstChild"); }
		}

		public bool isConnected {
			get { return API.GetProperty<bool>("isConnected"); }
		}

		public Node lastChild {
			get { return API.GetObject<Node>("lastChild"); }
		}

		public Node nextSibling {
			get { return API.GetObject<Node>("nextSibling"); }
		}

		public string nodeName {
			get { return API.GetProperty<string>("nodeName"); }
		}

		public int nodeType {
			get { return API.GetProperty<int>("nodeType"); }
		}

		/*
		public bool nodeValue {
			get { return API.GetProperty<bool>("nodeValue"); }
		}
		//*/

		/*
		public bool ownerDocument {
			get { return API.GetProperty<bool>("ownerDocument"); }
		}
		//*/

		public Node parentNode {
			get { return API.GetObject<Node>("parentNode"); }
		}

		public Element parentElement {
			get { return API.GetObject<Element>("parentElement"); }
		}

		public Node previousSibling {
			get { return API.GetObject<Node>("previousSibling"); }
		}

		/*
		public bool textContent {
			get { return API.GetProperty<bool>("textContent"); }
		}
		//*/

		/*
		public bool rootNode {
			get { return API.GetProperty<bool>("rootNode"); }
		}
		//*/

		public Node appendChild(Node aChild) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var node = {0}.appendChild({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				Script.GetObject(aChild.API.id),
				Script.AddObject("node")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Node>(id);
		}

		public Node cloneNode(bool deep) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var node = {0}.cloneNode({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				deep.Escape(),
				Script.AddObject("node")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Node>(id);
		}

		public int compareDocumentPosition(Node otherNode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.compareDocumentPosition({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(otherNode.API.id)
			);
			return API._ExecuteBlocking<int>(script);
		}

		public bool contains(Node otherNode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.contains({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(otherNode.API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public Node getRootNode(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var node = {0}.getRootNode({1});",
					"return {2}"
				),
				Script.GetObject(API.id),
				options.Stringify(),
				Script.AddObject("node")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Node>(id);
		}

		public bool hasChildNodes() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.hasChildNodes();"
				),
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public Node insertBefore(Node newNode, Node referenceNode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var node = {0}.insertBefore({1},{2});",
					"return {3}"
				),
				Script.GetObject(API.id),
				Script.GetObject(newNode.API.id),
				Script.GetObject(referenceNode.API.id),
				Script.AddObject("node")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Node>(id);
		}

		public bool isDefaultNamespace(string namespaceURI) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.isDefaultNamespace({1});"
				),
				Script.GetObject(API.id),
				namespaceURI.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool isEqualNode(Node arg) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.isEqualNode({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(arg.API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		[Obsolete]
		public bool isSameNode(Node arg) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.isSameNode({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(arg.API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public string lookupPrefix() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.lookupPrefix();"
				),
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<string>(script);
		}

		public string lookupNamespaceURI() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.lookupNamespaceURI();"
				),
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<string>(script);
		}

		public void normalize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.normalize();"
				),
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public Node removeChild(Node child) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var node = {0}.removeChild({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				Script.GetObject(child.API.id),
				Script.AddObject("node")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Node>(id);
		}

		public Node replaceChild(Node newChild, Node oldChild) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var node = {0}.replaceChild({1},{2});",
					"return {3};"
				),
				Script.GetObject(API.id),
				Script.GetObject(newChild.API.id),
				Script.GetObject(oldChild.API.id),
				Script.AddObject("node")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Node>(id);
		}
	}
}
