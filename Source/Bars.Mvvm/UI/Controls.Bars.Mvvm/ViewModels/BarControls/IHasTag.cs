namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that can store custom data on a <see cref="Tag"/> property.
	/// </summary>
	public interface IHasTag {

		/// <summary>
		/// Gets or sets a user-defined object attached to the control.
		/// </summary>
		object Tag { get; set; }

	}

}
