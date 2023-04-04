using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common {
	
	/// <summary>
	/// Represents a product item for displaying in the various panels.
	/// </summary>
	public partial class ProductListBoxItem : ListBoxItem {

		private static int counter = 0;

		#region Dependency Property Keys

		/// <summary>
		/// Identifies the <see cref="ProductFamily"/> dependency property key.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ProductFamily"/> dependency property key.</value>
		private static readonly DependencyPropertyKey ProductFamilyPropertyKey = DependencyPropertyEx.RegisterReadOnly("ProductFamily",
			typeof(ProductFamilyInfo), typeof(ProductListBoxItem), new FrameworkPropertyMetadata(null));

		#endregion // Dependency Property Keys

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="IsDockable"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsDockable"/> dependency property.</value>
		public static readonly DependencyProperty IsDockableProperty = DependencyProperty.RegisterAttached("IsDockable",
			typeof(bool), typeof(ProductListBoxItem), new FrameworkPropertyMetadata(false));

		/// <summary>
		/// Identifies the <see cref="IsMovable"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsMovable"/> dependency property.</value>
		public static readonly DependencyProperty IsMovableProperty = DependencyProperty.RegisterAttached("IsMovable",
			typeof(bool), typeof(ProductListBoxItem), new FrameworkPropertyMetadata(false));

		/// <summary>
		/// Identifies the read-only <see cref="ProductFamily"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="ProductFamily"/> dependency property.</value>
		public static readonly DependencyProperty ProductFamilyProperty = ProductListBoxItem.ProductFamilyPropertyKey.DependencyProperty;

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductListBoxItem"/> class.
		/// </summary>
		public ProductListBoxItem() {
			this.Id = counter++;

			var data = this.FindResource("ProductData") as ProductData;
			if (data != null)
				this.ProductFamily = data.ProductFamilies[this.Id % data.ProductFamilies.Count];

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>Click</c> event of the <c>deleteButton</c> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnDeleteButtonClick(object sender, RoutedEventArgs e) {
			ListBox parent = this.Parent as ListBox;
			if (null != parent)
				parent.Items.Remove(this);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the dock-bottom button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnDockBottomButtonClick(object sender, RoutedEventArgs e) {
			AnimatedDockPanel.SetDock(this, Dock.Bottom);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the dock-left button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnDockLeftButtonClick(object sender, RoutedEventArgs e) {
			AnimatedDockPanel.SetDock(this, Dock.Left);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the dock-right button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnDockRightButtonClick(object sender, RoutedEventArgs e) {
			AnimatedDockPanel.SetDock(this, Dock.Right);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the dock-top button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnDockTopButtonClick(object sender, RoutedEventArgs e) {
			AnimatedDockPanel.SetDock(this, Dock.Top);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the <c>moveLeftButton</c> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnMoveLeftButtonClick(object sender, RoutedEventArgs e) {
			ListBox parent = this.Parent as ListBox;
			if (null != parent) {
				int index = parent.Items.IndexOf(this);
				if (index > 0) {
					int selectedIndex = parent.SelectedIndex;
					parent.Items.RemoveAt(index);
					parent.Items.Insert(index - 1, this);

					if (selectedIndex == index)
						parent.SelectedIndex = index - 1;
				}
			}
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the <c>moveRightButton</c> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnMoveRightButtonClick(object sender, RoutedEventArgs e) {
			ListBox parent = this.Parent as ListBox;
			if (null != parent) {
				int index = parent.Items.IndexOf(this);
				if (index < parent.Items.Count - 1) {
					int selectedIndex = parent.SelectedIndex;
					parent.Items.RemoveAt(index);
					parent.Items.Insert(index + 1, this);

					if (selectedIndex == index)
						parent.SelectedIndex = index + 1;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the product item identifier.
		/// </summary>
		/// <value>The product item identifier.</value>
		public int Id {
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dockable.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is dockable; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsDockable {
			get { return (bool)this.GetValue(ProductListBoxItem.IsDockableProperty); }
			set { this.SetValue(ProductListBoxItem.IsDockableProperty, value); }
		}
	
		/// <summary>
		/// Gets or sets a value indicating whether this instance is movable.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is movable; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsMovable {
			get { return (bool)this.GetValue(ProductListBoxItem.IsMovableProperty); }
			set { this.SetValue(ProductListBoxItem.IsMovableProperty, value); }
		}

		/// <summary>
		/// Gets or sets the product family.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The product family.
		/// The default value is <c>null</c>.
		/// </value>
		public ProductFamilyInfo ProductFamily {
			get { return (ProductFamilyInfo)this.GetValue(ProductListBoxItem.ProductFamilyProperty); }
			private set { this.SetValue(ProductListBoxItem.ProductFamilyPropertyKey, value); }
		}
	}
}
