using System;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.BackgroundPicker {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Color baseColor;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.BaseColor = UIColor.FromWebColor("#ffbf40").ToColor();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Update all the brushes.
		/// </summary>
		private void UpdateBrushes() {
			if (listBox == null)
				return;

			// Calculate light/dark colors
			UIColor lightUIColor = UIColor.FromColor(baseColor);
			lightUIColor.HlsLightness = Math.Min(1.0, lightUIColor.HlsLightness + 0.25);
			UIColor darkUIColor = UIColor.FromColor(baseColor);
			darkUIColor.HlsLightness = Math.Max(0.0, darkUIColor.HlsLightness - 0.25);

			// Get all colors used
			Color lightColor = lightUIColor.ToColor();
			Color mediumColor = baseColor;
			Color darkColor = darkUIColor.ToColor();

			// Update brushes
			solidBrushData.Brush = new SolidColorBrush(mediumColor);

			vertLightToMediumBrushData.Brush = new LinearGradientBrush(lightColor, mediumColor, 90.0);
			vertLightToDarkBrushData.Brush = new LinearGradientBrush(lightColor, darkColor, 90.0);
			vertMediumToLightBrushData.Brush = new LinearGradientBrush(mediumColor, lightColor, 90.0);
			vertMediumToDarkBrushData.Brush = new LinearGradientBrush(mediumColor, darkColor, 90.0);
			vertDarkToLightBrushData.Brush = new LinearGradientBrush(darkColor, lightColor, 90.0);
			vertDarkToMediumBrushData.Brush = new LinearGradientBrush(darkColor, mediumColor, 90.0);
			
			horizLightToMediumBrushData.Brush = new LinearGradientBrush(lightColor, mediumColor, 0.0);
			horizLightToDarkBrushData.Brush = new LinearGradientBrush(lightColor, darkColor, 0.0);
			horizMediumToLightBrushData.Brush = new LinearGradientBrush(mediumColor, lightColor, 0.0);
			horizMediumToDarkBrushData.Brush = new LinearGradientBrush(mediumColor, darkColor, 0.0);
			horizDarkToLightBrushData.Brush = new LinearGradientBrush(darkColor, lightColor, 0.0);
			horizDarkToMediumBrushData.Brush = new LinearGradientBrush(darkColor, mediumColor, 0.0);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the base color.
		/// </summary>
		/// <value>The base color.</value>
		public Color BaseColor {
			get {
				return baseColor;
			}
			set {
				baseColor = value;
				this.UpdateBrushes();
			}
		}

	}
}