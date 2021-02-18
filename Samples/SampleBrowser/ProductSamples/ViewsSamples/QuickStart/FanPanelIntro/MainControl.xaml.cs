using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.FanPanelIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="BackAngleStep"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="BackAngleStep"/> dependency property.</value>
		public static readonly DependencyProperty BackAngleStepProperty = DependencyPropertyEx.Register("BackAngleStep",
			typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(15.0),
			delegate(object obj) { return ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, -360, 360); });

		/// <summary>
		/// Identifies the <see cref="BackOffsetStep"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="BackOffsetStep"/> dependency property.</value>
		public static readonly DependencyProperty BackOffsetStepProperty = DependencyPropertyEx.Register("BackOffsetStep",
			typeof(Point), typeof(MainControl), new FrameworkPropertyMetadata(new Point()));

		/// <summary>
		/// Identifies the <see cref="BackOpacityStep"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="BackOpacityStep"/> dependency property.</value>
		public static readonly DependencyProperty BackOpacityStepProperty = DependencyPropertyEx.Register("BackOpacityStep",
			typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(0.1),
			delegate(object obj) { return ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, 0, 1); });

		/// <summary>
		/// Identifies the <see cref="ForeAngleStep"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ForeAngleStep"/> dependency property.</value>
		public static readonly DependencyProperty ForeAngleStepProperty = DependencyPropertyEx.Register("ForeAngleStep",
			typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(15.0),
			delegate(object obj) { return ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, -360, 360); });

		/// <summary>
		/// Identifies the <see cref="ForeElementLayoutPlacement"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ForeElementLayoutPlacement"/> dependency property.</value>
		public static readonly DependencyProperty ForeElementLayoutPlacementProperty = DependencyPropertyEx.Register("ForeElementLayoutPlacement",
			typeof(ElementLayoutPlacement), typeof(MainControl), new FrameworkPropertyMetadata(ElementLayoutPlacement.Hidden));

		/// <summary>
		/// Identifies the <see cref="ForeOffsetStep"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ForeOffsetStep"/> dependency property.</value>
		public static readonly DependencyProperty ForeOffsetStepProperty = DependencyPropertyEx.Register("ForeOffsetStep",
			typeof(Point), typeof(MainControl), new FrameworkPropertyMetadata(new Point()));

		/// <summary>
		/// Identifies the <see cref="ForeOpacityStep"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ForeOpacityStep"/> dependency property.</value>
		public static readonly DependencyProperty ForeOpacityStepProperty = DependencyPropertyEx.Register("ForeOpacityStep",
			typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(0.1),
			delegate(object obj) { return ValidationHelper.ValidateDoubleIsBetweenInclusive(obj, 0, 1); });

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			for (int i = 0; i < 15; i++)
				this.OnAddItemClick(null, null);
			this.listBox.SelectedIndex = 2;

			// Show horizontal layout
			OnLayoutHorizontalButtonClick(this, null);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new <see cref="ProductListBoxItem"/> instance.
		/// </summary>
		/// <returns>A new <see cref="ProductListBoxItem"/> instance.</returns>
		private ProductListBoxItem CreateItem() {
			return new ProductListBoxItem() { IsMovable = true };
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the clear button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearAllClick(object sender, RoutedEventArgs e) {
			this.listBox.Items.Clear();
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the add button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemClick(object sender, RoutedEventArgs e) {
			this.listBox.Items.Add(CreateItem());
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the insert button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnInsertItemClick(object sender, RoutedEventArgs e) {
			int index = MathHelper.Range(this.listBox.SelectedIndex + 1, 0, this.listBox.Items.Count);
			this.listBox.Items.Insert(index, CreateItem());
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLayoutDefaultButtonClick(object sender, RoutedEventArgs e) {
			this.ForeAngleStep = 15.0;
			this.ForeElementLayoutPlacement = ElementLayoutPlacement.Hidden;
			this.ForeOffsetStep = new Point();
			this.ForeOpacityStep = 0.1;
			this.BackAngleStep = 15.0;
			this.BackOffsetStep = new Point();
			this.BackOpacityStep = 0.1;
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLayoutHorizontalButtonClick(object sender, RoutedEventArgs e) {
			this.ForeAngleStep = 10.0;
			this.ForeElementLayoutPlacement = ElementLayoutPlacement.Below;
			this.ForeOffsetStep = new Point(100, 0);
			this.ForeOpacityStep = 0.2;
			this.BackAngleStep = 10.0;
			this.BackOffsetStep = new Point(100, 0);
			this.BackOpacityStep = 0.2;
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLayoutTwistButtonClick(object sender, RoutedEventArgs e) {
			this.ForeAngleStep = 20.0;
			this.ForeElementLayoutPlacement = ElementLayoutPlacement.Below;
			this.ForeOffsetStep = new Point(10,10);
			this.ForeOpacityStep = 0.05;
			this.BackAngleStep = 20.0;
			this.BackOffsetStep = new Point(10, 10);
			this.BackOpacityStep = 0.05;
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLayoutVerticalButtonClick(object sender, RoutedEventArgs e) {
			this.ForeAngleStep = 0.0;
			this.ForeElementLayoutPlacement = ElementLayoutPlacement.Below;
			this.ForeOffsetStep = new Point(0, 125);
			this.ForeOpacityStep = 0.4;
			this.BackAngleStep = 0.0;
			this.BackOffsetStep = new Point(0, 125);
			this.BackOpacityStep = 0.4;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a value used in increment the angle of elements after the focal element based on their distance.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// A value used in increment the angle of elements after the focal element based on their distance.
		/// The default value is <c>15.0</c>.
		/// </value>
		public double BackAngleStep {
			get { return (double)this.GetValue(MainControl.BackAngleStepProperty); }
			set { this.SetValue(MainControl.BackAngleStepProperty, value); }
		}

		/// <summary>
		/// Gets or sets a value used in increment the X and Y coordinate of the elements after the focal element based on their distance.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// A value used in increment the X and Y coordinate of the elements after the focal element based on their distance.
		/// The default value is <c>0,0</c>.
		/// </value>
		public Point BackOffsetStep {
			get { return (Point)this.GetValue(MainControl.BackOffsetStepProperty); }
			set { this.SetValue(MainControl.BackOffsetStepProperty, value); }
		}

		/// <summary>
		/// Gets or sets a value used in decrement the opacity of the elements after the focal element based on their distance.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// A value used in decrement the opacity of the elements after the focal element based on their distance.
		/// The default value is <c>0.1</c>.
		/// </value>
		public double BackOpacityStep {
			get { return (double)this.GetValue(MainControl.BackOpacityStepProperty); }
			set { this.SetValue(MainControl.BackOpacityStepProperty, value); }
		}

		/// <summary>
		/// Gets or sets a value used in increment the angle of elements before the focal element based on their distance.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// A value used in increment the angle of elements before the focal element based on their distance.
		/// The default value is <c>15.0</c>.
		/// </value>
		public double ForeAngleStep {
			get { return (double)this.GetValue(MainControl.ForeAngleStepProperty); }
			set { this.SetValue(MainControl.ForeAngleStepProperty, value); }
		}

		/// <summary>
		/// Gets or sets the placement of any elements after the focal element in the panel.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The placement of any elements after the focal element in the panel.
		/// The default value is <c>ElementLayoutPlacement.Hidden</c>.
		/// </value>
		public ElementLayoutPlacement ForeElementLayoutPlacement {
			get { return (ElementLayoutPlacement)this.GetValue(MainControl.ForeElementLayoutPlacementProperty); }
			set { this.SetValue(MainControl.ForeElementLayoutPlacementProperty, value); }
		}

		/// <summary>
		/// Gets or sets a value used in increment the X and Y coordinate of the elements before the focal element based on their distance.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// A value used in increment the X and Y coordinate of the elements before the focal element based on their distance.
		/// The default value is <c>0,0</c>.
		/// </value>
		public Point ForeOffsetStep {
			get { return (Point)this.GetValue(MainControl.ForeOffsetStepProperty); }
			set { this.SetValue(MainControl.ForeOffsetStepProperty, value); }
		}

		/// <summary>
		/// Gets or sets a value used in decrement the opacity of the elements before the focal element based on their distance.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// A value used in decrement the opacity of the elements before the focal element based on their distance.
		/// The default value is <c>0.1</c>.
		/// </value>
		public double ForeOpacityStep {
			get { return (double)this.GetValue(MainControl.ForeOpacityStepProperty); }
			set { this.SetValue(MainControl.ForeOpacityStepProperty, value); }
		}

	}
}