using System;
using System.Collections.Generic;
#if WINRT
using ActiproSoftware.ProductSamples.Charts.Common;
using ActiproSoftware.UI.Xaml.Controls.Charts.Palettes;
#else
using ActiproSoftware.ProductSamples.Charts.Common;
using ActiproSoftware.Windows.Controls.Charts.Palettes;
using ActiproSoftware.Windows.Data;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Palettes {


	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private IList<SeriesPaletteStyleSelector> styleSelectors;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainControl"/> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Builds the style selectors.
		/// </summary>
		/// <returns>The style selectors.</returns>
		private List<SeriesPaletteStyleSelector> BuildStyleSelectors() {
			Array enumValues = new EnumValueProvider(typeof(PaletteKind)).EnumValues;

			var selectors = new List<SeriesPaletteStyleSelector>();
			foreach (Enum value in enumValues) {
				var palette = new Palette((PaletteKind)value);
				var styleSelector = new SeriesPaletteStyleSelector(palette);
				selectors.Add(styleSelector);
			}

			return selectors;
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of style selectors that shows off the available palettes.
		/// </summary>
		/// <value>The collection of style selectors that shows off the available palettes.</value>
		public IEnumerable<SeriesPaletteStyleSelector> StyleSelectors {
			get {
				if (styleSelectors == null)
					styleSelectors = BuildStyleSelectors();

				return styleSelectors;
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
