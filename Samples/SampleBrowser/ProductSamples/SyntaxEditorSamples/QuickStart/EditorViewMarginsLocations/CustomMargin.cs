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

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsLocations {

	/// <summary>
	/// Represents an implementation of a custom margin for an <see cref="IEditorView"/>.
	/// </summary>
	public class CustomMargin : Control, IEditorViewMargin {

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
		/// <param name="placement">A <see cref="EditorViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IEditorView"/>.</param>
		public CustomMargin(EditorViewMarginPlacement placement) : this() {
			this.Placement = placement;
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
				// Make this custom margin appear "outside" of all the built-in margins
				return new Ordering[] {
					new Ordering(EditorViewMarginKeys.Indicator, OrderPlacement.After),
					new Ordering(EditorViewMarginKeys.LineNumber, OrderPlacement.After),
					new Ordering(EditorViewMarginKeys.Selection, OrderPlacement.After),
					new Ordering(EditorViewMarginKeys.Ruler, OrderPlacement.After),
					new Ordering(EditorViewMarginKeys.WordWrapGlyph, OrderPlacement.After),
				};
			}
		}		
		
		/// <summary>
		/// Gets or sets a <see cref="EditorViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IEditorView"/>.
		/// </summary>
		/// <value>A <see cref="EditorViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IEditorView"/>.</value>
		public EditorViewMarginPlacement Placement { get; set; }
		
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