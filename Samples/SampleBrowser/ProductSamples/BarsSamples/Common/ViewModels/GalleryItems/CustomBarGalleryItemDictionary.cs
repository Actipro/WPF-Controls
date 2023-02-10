using ActiproSoftware.Windows.Controls.Bars;
using System;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a <see cref="ResourceDictionary"/> related to the custom <see cref="BarGalleryItem"/> resources objects defined in this assembly.
	/// </summary>
	public sealed partial class CustomBarGalleryItemDictionary : ResourceDictionary {

		[ThreadStatic]
		private static CustomBarGalleryItemDictionary instance;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomBarGalleryItemDictionary"/> class.
		/// </summary>
		public CustomBarGalleryItemDictionary() {
			this.InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the singleton instance of the resource dictionary.
		/// </summary>
		/// <value>The singleton instance of the resource dictionary.</value>
		/// <remarks>
		/// The instance is not shared between threads.
		/// </remarks>
		public static CustomBarGalleryItemDictionary Instance {
			get {
				if (instance is null)
					instance = new CustomBarGalleryItemDictionary();
				return instance;
			}
		}

	}

}
