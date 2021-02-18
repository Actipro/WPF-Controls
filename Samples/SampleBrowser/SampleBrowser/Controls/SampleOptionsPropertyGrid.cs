using ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;
using System.Windows;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Represents a <see cref="PropertyGrid"/> used to configure sample options.
	/// </summary>
	public class SampleOptionsPropertyGrid : PropertyGrid {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleOptionsPropertyGrid"/> class.
		/// </summary>
		public SampleOptionsPropertyGrid() {
			this.DefaultStyleKey = typeof(SampleOptionsPropertyGrid);

			// Adjust pre-defined columns
			this.Columns[0].CellBorderThickness = new Thickness();
			this.Columns[1].CellPadding = new Thickness(0, 2, 0, 2);

			// Use Grids/Editors integration
			BuiltinPropertyEditors.SetIsEnabled(this, true);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs after the value is set for an <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="e">The <c>PropertyModelValueChangeEventArgs</c> that contains the event data.</param>
		protected override void OnPropertyValueChanged(PropertyModelValueChangeEventArgs e) {
			// Call the base method
			base.OnPropertyValueChanged(e);

			// Ensure the target element is scrolled into view
			var element = e.PropertyModel.Target as FrameworkElement;
			if (element != null)
				element.BringIntoView();
		}

	}

}
