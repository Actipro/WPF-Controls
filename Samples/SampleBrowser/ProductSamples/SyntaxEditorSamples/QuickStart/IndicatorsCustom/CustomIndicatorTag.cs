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

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsCustom {
   
	/// <summary>
	/// Represents an <see cref="IIndicatorTag"/> that renders a custom indicator over a text range.
	/// </summary>
	public class CustomIndicatorTag : IndicatorClassificationTagBase {
	
		private static IClassificationType customIndicatorClassificationType = new ClassificationType("Custom Indicator");
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomIndicatorTag</c> class.
		/// </summary>
		static CustomIndicatorTag() {
			// Ensure the classification type is registered
			AmbientHighlightingStyleRegistry.Instance.Register(customIndicatorClassificationType, 
				new HighlightingStyle() { 
					Background = Color.FromArgb(0x40, 0x8a, 0xf3, 0x82),
					Foreground = Color.FromArgb(0xff, 0x00, 0x40, 0x00), 
				});
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

			context.FillEllipse(new Rect(x, y, diameter, diameter), Color.FromArgb(0xff, 0x8a, 0xf3, 0x82));
			context.DrawEllipse(new Rect(x, y, diameter, diameter), Color.FromArgb(0xff, 0x00, 0x40, 0x00), LineKind.Solid, 1);
		}

	}
	
}
