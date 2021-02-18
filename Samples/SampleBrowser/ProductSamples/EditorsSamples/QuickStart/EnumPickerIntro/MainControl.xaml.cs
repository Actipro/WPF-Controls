using System.Windows;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.EnumPickerIntro {

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