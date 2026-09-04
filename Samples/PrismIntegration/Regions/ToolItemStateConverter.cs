using System.Globalization;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;

namespace ActiproSoftware.Windows.PrismIntegration.Regions;

//
// NOTE: This converter and the related ToolItemState enum can be used in scenarios where you don't wish for your models to directly 
//       reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
//       is nothing wrong with directly referencing DockingWindowState in your VM class to avoid having to use this abstraction layer
//

/// <summary>
/// Represents a value converter that can convert a <see cref="ToolItemState"/> to a <see cref="DockingWindowState"/>.
/// </summary>
public sealed class ToolItemStateConverter : IValueConverter {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static object? Convert(object? value, Type targetType) {
		if (targetType == typeof(DockingWindowState)) {
			if (value is DockingWindowState typedValue)
				return typedValue;
			else if (value is ToolItemState otherState) {
				return otherState switch {
					ToolItemState.AutoHide => DockingWindowState.AutoHide,
					ToolItemState.Docked => DockingWindowState.Docked,
					ToolItemState.Document or _ => DockingWindowState.Document
				};
			}
		}
		else if (targetType == typeof(ToolItemState)) {
			if (value is ToolItemState typedValue)
				return typedValue;
			else if (value is DockingWindowState otherState) {
				return otherState switch {
					DockingWindowState.AutoHide => ToolItemState.AutoHide,
					DockingWindowState.Docked => ToolItemState.Docked,
					DockingWindowState.Document or _ => ToolItemState.Document
				};
			}
		}
		return null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> Convert(value, targetType);

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> Convert(value, targetType);

}
