using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsCustom {

	/// <summary>
	/// Represents an implementation of a custom margin for an <see cref="IEditorView"/>.
	/// </summary>
	public class CustomMargin : Control, IEditorViewMargin {
		
		private IEditorView			view;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomMargin</c> class.
		/// </summary>
		static CustomMargin() {
			// Override property defaults
			Control.IsTabStopProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(false));
			Control.PaddingProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(new Thickness(5)));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(typeof(CustomMargin)));
			FrameworkElement.FocusableProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(false));
		}
		
		/// <summary>
		/// Initializes an instance of the <c>CustomMargin</c> class.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that will host the margin.</param>
		public CustomMargin(IEditorView view) {
			// Initialize 
			this.view = view;

			// Attach to events
			view.MarginsDestroyed += OnViewMarginsDestroyed;
			view.TextAreaLayout += OnViewTextAreaLayout;
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
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the view's margins are destroyed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
		private void OnViewMarginsDestroyed(object sender, EventArgs e) {
			// Detach from events
			view.MarginsDestroyed -= OnViewMarginsDestroyed;
			view.TextAreaLayout -= OnViewTextAreaLayout;
		}
			
		/// <summary>
		/// Occurs when a view text area layout occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="TextViewTextAreaLayoutEventArgs"/> that contains the event data.</param>
		private void OnViewTextAreaLayout(object sender, TextViewTextAreaLayoutEventArgs e) {
			if (this.Visibility == Visibility.Visible) {
				// Determine min width
				int digitCount = view.CurrentSnapshot.Length.ToString(CultureInfo.CurrentCulture).Length;

				// Get typeface
				Typeface typeface = new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch);
				double fontSize = this.FontSize;

				// Get the formatted text
				FormattedText text = new FormattedText(new string('0', digitCount) + " chars", CultureInfo.CurrentCulture, 
					FlowDirection.LeftToRight, typeface, fontSize, this.Foreground, VisualTreeHelper.GetDpi(view.VisualElement).PixelsPerDip);

				// Update the min width to ensure all digits will fit when on the last line
				double minWidth = Math.Max(42, Math.Ceiling(text.WidthIncludingTrailingWhitespace) + this.Padding.Left + this.Padding.Right);
				if (this.MinWidth != minWidth)
					this.MinWidth = minWidth;
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
			Rect marginBounds = context.Bounds;

			// Loop through all the view lines
			ITextViewLineCollection visibleLines = view.VisibleViewLines;
			foreach (ITextViewLine viewLine in visibleLines) {
				// Get bounds relative to this element
				Rect bounds = view.TransformFromTextArea(viewLine.Bounds);

				// Get the number of characters on the line
				string characterCount = viewLine.CharacterCount.ToString(CultureInfo.CurrentCulture) + " chars";

				// Get the foreground
				var foreground = Colors.Black;
				if (viewLine.CharacterCount > 60)
					foreground = Colors.Red;
				else if (viewLine.CharacterCount > 40)
					foreground = Colors.DarkGoldenrod;
				else if (viewLine.CharacterCount > 20)
					foreground = Colors.DarkGreen;

				// Get the line layout
				var firstLayoutLine = context.Canvas.CreateTextLayout(characterCount, 0, this.FontFamily.Source, (float)this.FontSize, foreground).Lines[0];

				// Get x/y
				double x = marginBounds.Right - firstLayoutLine.Width - this.Padding.Right;
				double y = marginBounds.Y + viewLine.TextBounds.Y + (int)Math.Round(viewLine.Baseline - firstLayoutLine.Baseline, MidpointRounding.AwayFromZero);

				// Draw the text
				context.DrawText(new Point(x, y), firstLayoutLine);
			}
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
				};
			}
		}		
		
		/// <summary>
		/// Gets or sets a <see cref="EditorViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IEditorView"/>.
		/// </summary>
		/// <value>A <see cref="EditorViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IEditorView"/>.</value>
		public EditorViewMarginPlacement Placement { 
			get {
				return EditorViewMarginPlacement.ScrollableLeft;
			}
		}
		
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