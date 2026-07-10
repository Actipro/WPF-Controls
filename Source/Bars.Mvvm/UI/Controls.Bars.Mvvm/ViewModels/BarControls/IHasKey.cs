namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Provides the base requirements for a control that has a <see cref="Key"/> property.
/// </summary>
public interface IHasKey {

	/// <summary>
	/// A string that uniquely identifies the control.
	/// </summary>
	string? Key { get; }

}
