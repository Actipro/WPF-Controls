using System;
using System.Collections.Generic;
using ActiproSoftware.Windows.Controls.MicroCharts.Palettes;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.Palettes {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private IList<MicroSeriesPaletteStyleSelector> styleSelectors;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of style selectors that shows off the available palettes.
		/// </summary>
		/// <value>The collection of style selectors that shows off the available palettes.</value>
		public IEnumerable<MicroSeriesPaletteStyleSelector> StyleSelectors {
			get {
				if (styleSelectors == null) {
					styleSelectors = new List<MicroSeriesPaletteStyleSelector>();
					foreach (Enum value in Enum.GetValues(typeof(MicroPaletteKind))) {
						MicroPalette palette = new MicroPalette((MicroPaletteKind)value);
						MicroSeriesPaletteStyleSelector styleSelector = new MicroSeriesPaletteStyleSelector(palette);
						styleSelectors.Add(styleSelector);
					}
				}
				return styleSelectors;
			}
		}

	}
}