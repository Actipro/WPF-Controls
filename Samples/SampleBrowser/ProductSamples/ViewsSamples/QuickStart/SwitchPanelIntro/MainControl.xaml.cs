using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.SwitchPanelIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Random random = new Random(Environment.TickCount);

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="ActiveIndex"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ActiveIndex"/> dependency property.</value>
		public static readonly DependencyProperty ActiveIndexProperty = DependencyProperty.Register("ActiveIndex",
			typeof(int), typeof(MainControl), new FrameworkPropertyMetadata(0, OnActiveIndexPropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.Loaded += new RoutedEventHandler(this.OnLoaded);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new <see cref="ProductListBoxItem"/> instance.
		/// </summary>
		/// <returns>A new <see cref="ProductListBoxItem"/> instance.</returns>
		private ProductListBoxItem CreateItem() {
			bool isDock = (1 == this.ActiveIndex);

			ProductListBoxItem item = new ProductListBoxItem() { IsDockable = isDock, IsMovable = !isDock };

			// Randomly place the item in Canvas
			double left = random.NextDouble() * (this.listBox.ActualWidth - item.MinWidth);
			double top = random.NextDouble() * (this.listBox.ActualHeight - item.MinHeight);
			AnimatedCanvas.SetLeft(item, left);
			AnimatedCanvas.SetTop(item, top);

			return item;
		}

		/// <summary>
		/// Occurs when the <see cref="ActiveIndexProperty"/> value is changed.
		/// </summary>
		/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnActiveIndexPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			MainControl control = d as MainControl;
			if (null != control)
				control.UpdateListBox();
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
		/// Handles the <c>Click</c> event of the clear button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearAllClick(object sender, RoutedEventArgs e) {
			this.listBox.Items.Clear();
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
		/// Handles the <c>Loaded</c> event of the MainControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			for (int i = 0; i < 3; i++)
				this.OnAddItemClick(null, null);
			this.listBox.SelectedIndex = 0;

			this.UpdateListBox();
		}

		/// <summary>
		/// Updates the list box based on the current options.
		/// </summary>
		private void UpdateListBox() {
			if (null != this.listBox) {
				// Update the scroll bars
				switch (this.ActiveIndex) {
					case 0: // AnimatedCanvas
					case 1: // AnimatedDockPanel
						this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
						this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
						break;

					case 2: // AnimatedStackPanel (Horizontal)
					case 6: // AnimatedWrapPanel (Vertical)
					case 7: // AnimatedWrapPanel (Vertical + Evenly Spaced)
						this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
						this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
						break;

					case 3: // AnimatedStackPanel (Vertical)
					case 4: // AnimatedWrapPanel (Horizontal)
					case 5: // AnimatedWrapPanel (Horizontal + Evenly Spaced)
						this.listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
						this.listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
						break;
				}

				// Update the item states
				bool isDock = (1 == this.ActiveIndex);
				foreach (ProductListBoxItem element in this.listBox.Items) {
					element.IsDockable = isDock;
					element.IsMovable = !isDock;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the active index of the <see cref="SwitchPanel"/>.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The active index of the <see cref="SwitchPanel"/>.
		/// The default value is <c>0</c>.
		/// </value>
		public int ActiveIndex {
			get { return (int)this.GetValue(MainControl.ActiveIndexProperty); }
			set { this.SetValue(MainControl.ActiveIndexProperty, value); }
		}
	}
}