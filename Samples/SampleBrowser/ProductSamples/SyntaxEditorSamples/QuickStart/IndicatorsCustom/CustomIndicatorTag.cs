using System;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.Rendering;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom {
   
	/// <summary>
	/// Represents an <see cref="IIndicatorTag"/> that renders a custom indicator over a text range.
	/// </summary>
	public class CustomIndicatorTag : IndicatorClassificationTagBase {
	
		private static readonly IClassificationType customIndicatorClassificationType = new ClassificationType("Custom Indicator");
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomIndicatorTag</c> class.
		/// </summary>
		static CustomIndicatorTag() {
			// This sample assumes the editor will use the AmbientHighlightingStyleRegistry
			var registry = AmbientHighlightingStyleRegistry.Instance;

			// Configure light/dark color palettes with default colors
			var key = customIndicatorClassificationType.Key;
			registry.LightColorPalette?.SetForeground(key, UIColor.FromWebColor("#004000"));
			registry.LightColorPalette?.SetBackground(key, UIColor.FromWebColor("#ebf1dd"));
			registry.DarkColorPalette?.SetForeground(key, UIColor.FromWebColor("#95db7d"));
			registry.DarkColorPalette?.SetBackground(key, UIColor.FromWebColor("#265e4d"));

			// Associate a default style with the classification type
			// and the current color palette color will be automatically applied
			registry.Register(customIndicatorClassificationType, new HighlightingStyle());
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="IClassificationType"/> associated with the tag.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> associated with the tag.</value>
		public override IClassificationType ClassificationType { 
			get {
				return customIndicatorClassificationType;
			}
		}
	
		/// <summary>
		/// Draws the indicator's glyph in an editor view margin.
		/// </summary>
		/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
		/// <param name="viewLine">The <see cref="ITextViewLine"/> for which the glyph is rendered.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		/// <param name="bounds">The bounds in which the indicator will be rendered.</param>
		public override void DrawGlyph(TextViewDrawContext context, ITextViewLine viewLine, TagSnapshotRange<IIndicatorTag> tagRange, Rect bounds) {
			var diameter = Math.Max(8.0, Math.Min(13, Math.Round(Math.Min(bounds.Width, bounds.Height) - 2.0)));
			var x = bounds.X + (bounds.Width - diameter) / 2.0;
			var y = bounds.Y + (bounds.Height - diameter) / 2.0;

			// Create a circle glyph that uses the same foreground/background colors as the highlighting style
			var key = customIndicatorClassificationType.Key;
			var colorPalette = AmbientHighlightingStyleRegistry.Instance.CurrentColorPalette;
			context.FillEllipse(new Rect(x, y, diameter, diameter), colorPalette.GetBackground(key) ?? Colors.Green);
			context.DrawEllipse(new Rect(x, y, diameter, diameter), colorPalette.GetForeground(key) ?? Colors.DarkGreen, LineKind.Solid, 1);
		}

	}
	
}
