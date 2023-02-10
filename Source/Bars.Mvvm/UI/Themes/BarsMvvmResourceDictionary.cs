using System;
using System.Windows;

namespace ActiproSoftware.Windows.Themes {

	/// <summary>
	/// Represents a <see cref="ResourceDictionary"/> related to the template resources objects defined in this assembly.
	/// </summary>
	public sealed partial class BarsMvvmResourceDictionary : ResourceDictionary {

		[ThreadStatic]
		private static BarsMvvmResourceDictionary instance;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BarsMvvmResourceDictionary"/> class.
		/// </summary>
		public BarsMvvmResourceDictionary() {
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
		public static BarsMvvmResourceDictionary Instance
			=> instance ??= new BarsMvvmResourceDictionary();

	}

}
