using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedStackPanelIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Orientation"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Orientation"/> dependency property.</value>
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
			typeof(Orientation), typeof(MainControl), new FrameworkPropertyMetadata(Orientation.Vertical, OnOrientationPropertyValueChanged));
		
		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			for (int i = 0; i < 3; i++)
				this.OnAddItemClick(null, null);
			this.listBox.SelectedIndex = 0;

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
					this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
					this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
				}
				else {
					this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
					this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the orientation of the <see cref="AnimatedStackPanel"/>.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The orientation of the <see cref="AnimatedStackPanel"/>.
		/// The default value is <c>Orientation.Vertical</c>.
		/// </value>
		public Orientation Orientation {
			get { return (Orientation)this.GetValue(MainControl.OrientationProperty); }
			set { this.SetValue(MainControl.OrientationProperty, value); }
		}
	}
}