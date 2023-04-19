namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that is checkable.
	/// </summary>
	public interface ICheckable {
		
		/// <summary>
		/// Gets or sets whether the control is checked.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control is checked; otherwise, <c>false</c>.
		/// </value>
		bool IsChecked { get; set; }
		
	}

}
