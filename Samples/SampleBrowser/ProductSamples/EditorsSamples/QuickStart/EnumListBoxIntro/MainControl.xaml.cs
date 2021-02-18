using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.ProductSamples.EditorsSamples.Common;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.EnumListBoxIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : ProductItemControl {
		
		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="EnumWithFlags"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="EnumWithFlags"/> dependency property.</value>
		public static readonly DependencyProperty EnumWithFlagsProperty = DependencyProperty.Register("EnumWithFlags",
			typeof(EnumWithFlags?), typeof(MainControl), new PropertyMetadata(ActiproSoftware.ProductSamples.EditorsSamples.Common.EnumWithFlags.None));

		/// <summary>
		/// Identifies the <see cref="EnumWithoutFlags"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="EnumWithoutFlags"/> dependency property.</value>
		public static readonly DependencyProperty EnumWithoutFlagsProperty = DependencyProperty.Register("EnumWithoutFlags",
			typeof(EnumWithoutFlags?), typeof(MainControl), new PropertyMetadata(ActiproSoftware.ProductSamples.EditorsSamples.Common.EnumWithoutFlags.None));

		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region RandomEnumSortComparer

		/// <summary>
		/// Represents a random sort comparer for enumeration values.
		/// </summary>
		private class RandomEnumSortComparer : IComparer<Enum> {

			private Random random = new Random(Environment.TickCount);

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
			/// </summary>
			/// <param name="x">The first object to compare.</param>
			/// <param name="y">The second object to compare.</param>
			/// <returns>
			/// Value Condition
			/// Less than zero    - <paramref name="x"/> is less than <paramref name="y"/>.
			/// Zero              - <paramref name="x"/> equals <paramref name="y"/>.
			/// Greater than zero - <paramref name="x"/> is greater than <paramref name="y"/>.
			/// </returns>
			public int Compare(Enum x, Enum y) {
				// If equal, then 0 must be returned
				if (x == y)
					return 0;

				double r = random.NextDouble();
				if (r < 0.33)
					return -1;
				else if (r > 0.66)
					return 1;
				return 0;
			}

		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.DataContext = this;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains data related to the event.</param>
		private void OnSortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			var sortComboBox = (ComboBox)sender;
			switch (sortComboBox.SelectedIndex) {
				case 1:
					listBox1.EnumSortComparer = EnumValueNameSortComparer.Instance;
					break;
				case 2:
					listBox1.EnumSortComparer = new RandomEnumSortComparer();
					break;
				default:
					listBox1.EnumSortComparer = null;
					break;
			}
			listBox2.EnumSortComparer = listBox1.EnumSortComparer;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets an enumeration value that has the flags attribute.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The enumeration value that has the flags attribute.
		/// The default value is <c>EnumWithFlags.None</c>.
		/// </value>
		public EnumWithFlags? EnumWithFlags {
			get { return (EnumWithFlags?)this.GetValue(MainControl.EnumWithFlagsProperty); }
			set { this.SetValue(MainControl.EnumWithFlagsProperty, value); }
		}

		/// <summary>
		/// Gets or sets an enumeration value that does not have the flags attribute.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The enumeration value that does not have the flags attribute.
		/// The default value is <c>EnumWithoutFlags.None</c>.
		/// </value>
		public EnumWithoutFlags? EnumWithoutFlags {
			get { return (EnumWithoutFlags?)this.GetValue(MainControl.EnumWithoutFlagsProperty); }
			set { this.SetValue(MainControl.EnumWithoutFlagsProperty, value); }
		}

	}
}