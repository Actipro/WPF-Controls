namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that has a <see cref="Key"/> property.
	/// </summary>
	public interface IHasKey {
		
		/// <summary>
		/// Gets a string that uniquely identifies the control.
		/// </summary>
		/// <value>A string that uniquely identifies the control.</value>
		string Key { get; }
		
	}

}
