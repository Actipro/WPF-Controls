using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationMvvm {

	public interface ISampleCommands {

		/// <summary>
		/// Gets the command to restore the configured layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		ICommand RestoreLayout { get; }

		/// <summary>
		/// Gets the command to restore the original layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		ICommand RestoreOriginalLayout { get; }

		/// <summary>
		/// Gets the command to save the current layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		ICommand SaveLayout { get; }

	}

}
