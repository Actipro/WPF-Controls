using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Represents an <see cref="ItemsControl"/> that renders a control gallery in a sample.
	/// </summary>
	public class SampleGalleryControl : ItemsControl {
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="Label"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Label"/> dependency property.</value>
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(SampleGalleryControl), new FrameworkPropertyMetadata("GALLERY"));
		
		/// <summary>
		/// Identifies the <see cref="UseLowerContrast"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="UseLowerContrast"/> dependency property.</value>
		public static readonly DependencyProperty UseLowerContrastProperty = DependencyProperty.Register("UseLowerContrast", typeof(bool), typeof(SampleGalleryControl), new FrameworkPropertyMetadata(true));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleGalleryControl"/> class.
		/// </summary>
		public SampleGalleryControl() {
			this.DefaultStyleKey = typeof(SampleGalleryControl);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		/// <value>
		/// The label.
		/// </value>
		public string Label {
			get {
				return (string)this.GetValue(LabelProperty);
			}
			set {
				this.SetValue(LabelProperty, value);
			}
		}
		
		/// <summary>
		/// Prepares the specified element to display the specified item. 
		/// </summary>
		/// <param name="element">The <see cref="DependencyObject"/> that is the wrapper element.</param>
		/// <param name="item">The item that is being wrapped.</param>
		protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
			// Adjust the margin between children
			var container = element as FrameworkElement;
			if (container != null) {
				container.HorizontalAlignment = HorizontalAlignment.Left;
				container.Margin = new Thickness(0, 0, -this.Padding.Right, -this.Padding.Bottom);
				container.VerticalAlignment = VerticalAlignment.Top;
			}

			// Pass along contrast setting
			var card = element as LabeledCardControl;
			if (card != null)
				card.UseLowerContrast = this.UseLowerContrast;

			// Call the base method
			base.PrepareContainerForItemOverride(element, item);
		}
		
		/// <summary>
		/// Gets or sets whether to use lower contrast colors for the contained card backgrounds.
		/// </summary>
		/// <value>
		/// <c>true</c> if lower contrast colors should be used for the contained card backgrounds; otherwise, <c>false</c>.
		/// </value>
		public bool UseLowerContrast {
			get {
				return (bool)this.GetValue(UseLowerContrastProperty);
			}
			set {
				this.SetValue(UseLowerContrastProperty, value);
			}
		}

	}

}
