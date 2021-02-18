using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedWrapPanelIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="IsEmptySpaceEvenlyDistributed"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsEmptySpaceEvenlyDistributed"/> dependency property.</value>
		public static readonly DependencyProperty IsEmptySpaceEvenlyDistributedProperty = DependencyProperty.Register("IsEmptySpaceEvenlyDistributed",
			typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(false));

		/// <summary>
		/// Identifies the <see cref="Orientation"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Orientation"/> dependency property.</value>
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
			typeof(Orientation), typeof(MainControl), new FrameworkPropertyMetadata(Orientation.Horizontal, OnOrientationPropertyValueChanged));
		
		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			var data = this.FindResource("ProductData") as ProductData;
			if (data != null) {
				for (int i = 0; i < data.ProductFamilies.Count; i++)
					this.OnAddItemClick(null, null);
				this.listBox.SelectedIndex = 0;
			}

			this.UpdateListBox();
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
		/// Occurs when the <see cref="OrientationProperty"/> value is changed.
		/// </summary>
		/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnOrientationPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			MainControl control = d as MainControl;
			if (null != control)
				control.UpdateListBox();
		}

		/// <summary>
		/// Updates the list box based on the current options.
		/// </summary>
		private void UpdateListBox() {
			if (null != this.listBox) {
				if (this.Orientation == Orientation.Horizontal) {
					this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
					this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
				}
				else {
					this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
					this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a value indicating whether any empty space in a row/column will be evenly distributed around the elements.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// <c>true</c> if any empty space in a row/column will be evenly distributed around the elements; otherwise <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsEmptySpaceEvenlyDistributed {
			get { return (bool)this.GetValue(MainControl.IsEmptySpaceEvenlyDistributedProperty); }
			set { this.SetValue(MainControl.IsEmptySpaceEvenlyDistributedProperty, value); }
		}

		/// <summary>
		/// Gets or sets the orientation of the <see cref="AnimatedWrapPanel"/>.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The orientation of the <see cref="AnimatedWrapPanel"/>.
		/// The default value is <c>Orientation.Horizontal</c>.
		/// </value>
		public Orientation Orientation {
			get { return (Orientation)this.GetValue(MainControl.OrientationProperty); }
			set { this.SetValue(MainControl.OrientationProperty, value); }
		}
	}
}