using ActiproSoftware.Windows;
using System;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxIntro {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private Color color = Colors.LightSeaGreen;
		private DateTime date = DateTime.Today;
		private string comboboxPreviewLabel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the color in the editbox example.
		/// </summary>
		/// <value>The color in the editbox example.</value>
		public Color Color {
			get => color;
			set {
				if (color != value) {
					color = value;
					NotifyPropertyChanged(nameof(Color));
				}
			}
		}

		/// <summary>
		/// Gets or sets the label of the item currently being previewed in a combobox.
		/// </summary>
		/// <value>The label of the item.</value>
		public string ComboboxPreviewLabel {
			get => comboboxPreviewLabel;
			set {
				if (comboboxPreviewLabel != value) {
					comboboxPreviewLabel = value;
					NotifyPropertyChanged(nameof(ComboboxPreviewLabel));
				}
			}
		}

		/// <summary>
		/// Gets or sets the date in the editbox example.
		/// </summary>
		/// <value>The date in the editbox example.</value>
		public DateTime Date {
			get => date;
			set {
				if (date != value) {
					date = value;
					NotifyPropertyChanged(nameof(Date));
				}
			}
		}

	}

}
