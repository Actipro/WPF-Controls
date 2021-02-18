using System;
using System.Windows.Media;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.BackgroundPicker {
	
	/// <summary>
	/// Stores brush data.
	/// </summary>
	public class BrushData : ObservableObjectBase {

		private Brush brush;
		private string description;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="Brush"/>.
		/// </summary>
		/// <value>The <see cref="Brush"/>.</value>
		public Brush Brush { 
			get {
				return brush;
			}
			set {
				brush = value;
				this.NotifyPropertyChanged("Brush");
			}
		}

		/// <summary>
		/// Gets or sets the description of the brush.
		/// </summary>
		/// <value>The description of the brush.</value>
		public string Description { 
			get {
				return description;
			}
			set {
				description = value;
				this.NotifyPropertyChanged("Description");
			}
		}

	}

}
