namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationMvvm;

public interface ISampleCommands {

	/// <summary>
	/// The command to restore the configured layout.
	/// </summary>
	ICommand RestoreLayout { get; }

	/// <summary>
	/// The command to restore the original layout.
	/// </summary>
	ICommand RestoreOriginalLayout { get; }

	/// <summary>
	/// The command to save the current layout.
	/// </summary>
	ICommand SaveLayout { get; }

}
