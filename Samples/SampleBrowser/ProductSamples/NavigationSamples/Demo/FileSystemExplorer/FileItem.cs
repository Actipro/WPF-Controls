using System;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Demo.FileSystemExplorer {

	/// <summary>
	/// Provides information about a file item.
	/// </summary>
	public class FileItem {

		private string		modified;
		private string		name;
		private string		size;
		private string		type;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the modified date.
		/// </summary>
		/// <value>The modified date.</value>
		public string Modified {
			get {
				return modified;
			}
			set {
				modified = value;
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public string Size {
			get {
				return size;
			}
			set {
				size = value;
			}
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public string Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}

	}
}
