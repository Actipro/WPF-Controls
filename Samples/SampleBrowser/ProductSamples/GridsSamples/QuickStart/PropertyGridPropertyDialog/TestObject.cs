using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyDialog {

	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject : ObservableObjectBase {
		
		private string editablePath		= @"C:\Documents\Foo.html";
		private string name				= "Foo";
		private string uneditablePath	= @"C:\Documents\Foo.css";
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
		[Description("A file path that can be typed in or selected via the ellipses button.")]
		public string EditablePath {
			get {
				return editablePath;
			}
			set {
				editablePath = value;
				this.NotifyPropertyChanged("EditablePath");
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[Description("The name of the item.")]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				this.NotifyPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Gets a read-only path.
		/// </summary>
		/// <value>The path.</value>
		[Description("A file path whose property is read-only, but keeps the ellipses button enabled for full display.  This concept is useful for getter-only collection properties.")]
		public string ReadOnlyPath {
			get {
				return @"C:\Documents\Foo.js";
			}
		}
		
		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
		[Description("Another file path but one that can't be directly typed in, only selected via the ellipses button.")]
		public string UneditablePath {
			get {
				return uneditablePath;
			}
			set {
				uneditablePath = value;
				this.NotifyPropertyChanged("UneditablePath");
			}
		}

	}

}
