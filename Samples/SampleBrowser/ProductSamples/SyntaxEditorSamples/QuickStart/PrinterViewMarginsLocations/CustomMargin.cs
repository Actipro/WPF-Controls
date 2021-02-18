using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrinterViewMarginsLocations {

	/// <summary>
	/// Represents an implementation of a custom margin for an <see cref="IPrinterView"/>.
	/// </summary>
	public class CustomMargin : Control, IPrinterViewMargin {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomMargin</c> class.
		/// </summary>
		static CustomMargin() {
			// Override property defaults
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(typeof(CustomMargin)));
		}
		
		/// <summary>
		/// Initializes an instance of the <c>CustomMargin</c> class.
		/// </summary>
		public CustomMargin() {}
		
		/// <summary>
		/// Initializes an instance of the <c>CustomMargin</c> class.
		/// </summary>
		/// <param name="view">The <see cref="IPrinterView"/> that will host the margins.</param>
		/// <param name="placement">A <see cref="PrinterViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IPrinterView"/>.</param>
		public CustomMargin(IPrinterView view, PrinterViewMarginPlacement placement) : this() {
			this.Placement = placement;

			// Get the style manually from the resources above the SyntaxEditor since this margin will be used
			//   outside the resource scope of the SyntaxEditor when displayed in a print preview...
			//   Alternatively, define a global style for this type in the application resources
			this.Style = view.SyntaxEditor.FindResource(typeof(CustomMargin)) as Style;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the string-based key that identifies the object.
		/// </summary>
		/// <value>The string-based key that identifies the object.</value>
		string IKeyedObject.Key { 
			get {
				return "Custom";
			}
		}		

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Draws the margin and its content.
		/// </summary>
		/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
		public void Draw(TextViewDrawContext context) {
			// NOTE: This margin is rendered via XAML but could be drawn here instead if desired
		}

		/// <summary>
		/// Gets the collection of <see cref="Ordering"/> objects, used to determine how this object is positioned relative to other objects.
		/// </summary>
		/// <value>The collection of <see cref="Ordering"/> objects, used to determine how this object is positioned relative to other objects.</value>
		public IEnumerable<Ordering> Orderings { 
			get {
				return new Ordering[] {
					new Ordering(PrinterViewMarginKeys.DocumentTitle, OrderPlacement.Before),
					new Ordering(PrinterViewMarginKeys.LineNumber, OrderPlacement.Before),
					new Ordering(PrinterViewMarginKeys.PageNumber, OrderPlacement.Before),
					new Ordering(PrinterViewMarginKeys.WordWrapGlyph, OrderPlacement.After),
				};
			}
		}		
		
		/// <summary>
		/// Gets or sets a <see cref="PrinterViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IPrinterView"/>.
		/// </summary>
		/// <value>A <see cref="PrinterViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IPrinterView"/>.</value>
		public PrinterViewMarginPlacement Placement { get; set; }
		
		/// <summary>
		/// Gets the <see cref="FrameworkElement"/> the is used to render this margin in the user interface.
		/// </summary>
		/// <value>The <see cref="FrameworkElement"/> the is used to render this margin in the user interface.</value>
		public FrameworkElement VisualElement { 
			get {
				return this;
			}
		}

	}
}