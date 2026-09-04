using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

//
// NOTE: This converter and the related ToolItemState enum can be used in scenarios where you don't wish for your models to directly
//   reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
//   is nothing wrong with directly referencing DockingWindowState in your VM class to avoid having to use this abstraction layer
//

/// <summary>
/// Represents a value converter that can convert a <see cref="ToolItemState"/> to a <see cref="DockingWindowState"/>.
/// </summary>
public sealed class ToolItemStateConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var state = (ToolItemState)value!;
		return state switch {
			ToolItemState.AutoHide => DockingWindowState.AutoHide,
			ToolItemState.Docked => DockingWindowState.Docked,
			ToolItemState.Document or _ => DockingWindowState.Document
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var state = (DockingWindowState)value!;
		return state switch {
			DockingWindowState.AutoHide => ToolItemState.AutoHide,
			DockingWindowState.Docked => ToolItemState.Docked,
			DockingWindowState.Document or _ => ToolItemState.Document
		};
	}

}
