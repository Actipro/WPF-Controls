using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements <see cref="ContentControl"/> that is part of a bullet list.
	/// </summary>
	public class BulletContentControl : ContentControl {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="BulletContentControl"/> class.
		/// </summary>
		public BulletContentControl() {
			this.DefaultStyleKey = typeof(BulletContentControl);
		}
		
	}

}
