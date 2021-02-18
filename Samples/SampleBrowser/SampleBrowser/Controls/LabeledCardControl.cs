using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements <see cref="Control"/> that renders its content in a card.
	/// </summary>
	[ContentProperty("Child")]
	public class LabeledCardControl : Control {
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="Child"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Child"/> dependency property.</value>
		public static readonly DependencyProperty ChildProperty = DependencyProperty.Register("Child", typeof(UIElement), typeof(LabeledCardControl), new FrameworkPropertyMetadata(null));
		
		/// <summary>
		/// Identifies the <see cref="Label"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Label"/> dependency property.</value>
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabeledCardControl), new FrameworkPropertyMetadata(null));
		
		/// <summary>
		/// Identifies the <see cref="LabelBackground"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="LabelBackground"/> dependency property.</value>
		public static readonly DependencyProperty LabelBackgroundProperty = DependencyProperty.Register("LabelBackground", typeof(Brush), typeof(LabeledCardControl), new FrameworkPropertyMetadata(null));
		
		/// <summary>
		/// Identifies the <see cref="Orientation"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Orientation"/> dependency property.</value>
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(LabeledCardControl), new FrameworkPropertyMetadata(Orientation.Vertical));
		
		/// <summary>
		/// Identifies the <see cref="UseLowerContrast"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="UseLowerContrast"/> dependency property.</value>
		public static readonly DependencyProperty UseLowerContrastProperty = DependencyProperty.Register("UseLowerContrast", typeof(bool), typeof(LabeledCardControl), new FrameworkPropertyMetadata(false));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="LabeledCardControl"/> class.
		/// </summary>
		public LabeledCardControl() {
			this.DefaultStyleKey = typeof(LabeledCardControl);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the child element.
		/// </summary>
		/// <value>
		/// The child element.
		/// </value>
		public UIElement Child {
			get {
				return (UIElement)this.GetValue(ChildProperty);
			}
			set {
				this.SetValue(ChildProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the text label.
		/// </summary>
		/// <value>
		/// The text label.
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
		/// Gets or sets the text label background <see cref="Brush"/>.
		/// </summary>
		/// <value>
		/// The text label background <see cref="Brush"/>.
		/// </value>
		public Brush LabelBackground {
			get {
				return (Brush)this.GetValue(LabelBackgroundProperty);
			}
			set {
				this.SetValue(LabelBackgroundProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the layout orientation.
		/// </summary>
		/// <value>
		/// The layout orientation.
		/// </value>
		public Orientation Orientation {
			get {
				return (Orientation)this.GetValue(OrientationProperty);
			}
			set {
				this.SetValue(OrientationProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets whether to use lower contrast colors for the background.
		/// </summary>
		/// <value>
		/// <c>true</c> if lower contrast colors should be used for the background; otherwise, <c>false</c>.
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
