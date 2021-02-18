using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements <see cref="Control"/> that renders a screenshot.
	/// </summary>
	public class ScreenshotControl : Control {
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="ImageSource"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ImageSource"/> dependency property.</value>
		public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ScreenshotControl), new FrameworkPropertyMetadata(null));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ScreenshotControl"/> class.
		/// </summary>
		public ScreenshotControl() {
			this.DefaultStyleKey = typeof(ScreenshotControl);

			this.Loaded += this.OnLoaded;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			// Remove the control if it's not in the root window
			if (!(this.DataContext is ApplicationViewModel)) {
				var container = this.Parent as InlineUIContainer;
				if (container != null) {
					var para = container.Parent as Paragraph;
					if (para != null) {
						var document = para.Parent as FlowDocument;
						if (document != null)
							document.Blocks.Remove(para);
					}
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the image source.
		/// </summary>
		/// <value>
		/// The image source.
		/// </value>
		public ImageSource ImageSource {
			get {
				return (ImageSource)this.GetValue(ImageSourceProperty);
			}
			set {
				this.SetValue(ImageSourceProperty, value);
			}
		}
		
	}

}
