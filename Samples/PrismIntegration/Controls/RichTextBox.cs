using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace ActiproSoftware.Windows.PrismIntegration.Controls {

	/// <summary>
	/// Represents a <see cref="RichTextBox"/> that has some default property settings for use in the sample browser.
	/// </summary>
	public class RichTextBox : System.Windows.Controls.RichTextBox {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="DocumentUri"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="DocumentUri"/> dependency property.</value>
		public static readonly DependencyProperty DocumentUriProperty = DependencyProperty.Register("DocumentUri", typeof(Uri), typeof(RichTextBox), new FrameworkPropertyMetadata(null, OnDocumentUriPropertyValueChanged));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>RichTextBox</c> class.
		/// </summary>
		static RichTextBox() {
			TextBoxBase.AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(true));
			TextBoxBase.AcceptsTabProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(true));
			TextBoxBase.HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(ScrollBarVisibility.Visible));
			TextBoxBase.VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(ScrollBarVisibility.Visible));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="DocumentUriProperty"/> value is changed.
		/// </summary>
		/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnDocumentUriPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
			RichTextBox control = (RichTextBox)obj;
			try {
				if (control.DocumentUri != null)
					control.Document = Application.LoadComponent(control.DocumentUri) as FlowDocument;
			}
			catch {}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.
		/// </summary>
		/// <value>A <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.</value>
		public Uri DocumentUri {
			get {
				return (Uri)this.GetValue(RichTextBox.DocumentUriProperty);
			}
			set {
				this.SetValue(RichTextBox.DocumentUriProperty, value);
			}
		}

		/// <summary>
		/// Called when the rendered size of a control changes. 
		/// </summary>
		/// <param name="sizeInfo">Specifies the size changes.</param>
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
			// Call the base method
			base.OnRenderSizeChanged(sizeInfo);

			// Adjust the document's page width (since there is a WPF bug when used within a parent ScrollViewer with horizontal scroll capabilities)
			if (this.Document != null)
				this.Document.PageWidth = Math.Max(1, this.ActualWidth - this.BorderThickness.Left - this.Padding.Left - 
					this.BorderThickness.Right - this.Padding.Right - SystemParameters.VerticalScrollBarWidth);
		}

	}

}

