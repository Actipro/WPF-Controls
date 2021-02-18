using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.HighlightingStyleViewer {

	/// <summary>
	/// Interaction logic for HighlightingStyleEditor.xaml
	/// </summary>
	public partial class HighlightingStyleEditor : UserControl {
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="ClassificationType"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ClassificationType"/> dependency property.</value>
		public static readonly DependencyProperty ClassificationTypeProperty = DependencyProperty.Register("ClassificationType", typeof(IClassificationType), typeof(HighlightingStyleEditor), new FrameworkPropertyMetadata(null, OnPropertyChangedForRefresh));

		/// <summary>
		/// Identifies the <see cref="HighlightingStyleRegistry"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="HighlightingStyleRegistry"/> dependency property.</value>
		public static readonly DependencyProperty HighlightingStyleRegistryProperty = DependencyProperty.Register("HighlightingStyleRegistry", typeof(IHighlightingStyleRegistry), typeof(HighlightingStyleEditor), new FrameworkPropertyMetadata(null, OnPropertyChangedForRefresh));

		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>HighlightingStyleEditor</c> class.
		/// </summary>
		public HighlightingStyleEditor() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a property is changed that needs to refresh the control.
		/// </summary>
		/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnPropertyChangedForRefresh(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
			HighlightingStyleEditor control = (HighlightingStyleEditor)obj;

			// Get the style to edit
			IHighlightingStyle style = null;
			if ((control.ClassificationType != null) && (control.HighlightingStyleRegistry != null))
				style = control.HighlightingStyleRegistry[control.ClassificationType];

			// Update the data context
			control.DataContext = style;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="IClassificationType"/> for which to edit an <see cref="IHighlightingStyle"/>.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> for which to edit an <see cref="IHighlightingStyle"/>.</value>
		public IClassificationType ClassificationType {
			get {
				return (IClassificationType)this.GetValue(HighlightingStyleEditor.ClassificationTypeProperty);
			}
			set {
				this.SetValue(HighlightingStyleEditor.ClassificationTypeProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="IHighlightingStyleRegistry"/> to use.
		/// </summary>
		/// <value>The <see cref="IHighlightingStyleRegistry"/> to use.</value>
		public IHighlightingStyleRegistry HighlightingStyleRegistry {
			get {
				return (IHighlightingStyleRegistry)this.GetValue(HighlightingStyleEditor.HighlightingStyleRegistryProperty);
			}
			set {
				this.SetValue(HighlightingStyleEditor.HighlightingStyleRegistryProperty, value);
			}
		}
		
	}
}
