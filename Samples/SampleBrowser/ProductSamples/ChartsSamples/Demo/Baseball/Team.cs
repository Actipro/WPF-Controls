#if WINRT
using ActiproSoftware.UI.Xaml;
using Windows.UI;
#else
using System.Windows.Media;
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	/// <summary>
	/// A baseball team.
	/// </summary>
	public class Team : ObservableObjectBase {

		private Color color;
		private string name;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color {
			get { return color; }
			set {
				color = value;
				NotifyPropertyChanged("Color");
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get { return name; }
			set {
				name = value;
				NotifyPropertyChanged("Name");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
