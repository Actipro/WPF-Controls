using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.UserPromptIntro;

/// <summary>
/// Defines a sample command which displays a prompt to confirm user selection before submitting it.
/// </summary>
internal class ConfirmationCommand : ICommand {

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ICommand.CanExecuteChanged"/>
	event EventHandler? ICommand.CanExecuteChanged {
		add { /* Not used */ }
		remove { /* Not used */ }
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Finds the <see cref="UserPromptControl"/> associated with a <see cref="DependencyObject"/>.
	/// </summary>
	/// <param name="searchObject">The object from which the search will begin.</param>
	private static UserPromptControl? FindUserPromptControl(DependencyObject? searchObject)
		=> searchObject.FindAncestorOfType<UserPromptControl>(includeSelf: true);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ICommand.CanExecute(object)"/>
	public bool CanExecute(object? parameter) {
		// Always allow the command
		return true;
	}

	/// <inheritdoc cref="ICommand.Execute(object)"/>
	public void Execute(object? parameter) {
		// The button on the user prompt must be passed as a parameter to this command
		if (parameter is Button button) {
			// Get the result which is configured for the button
			var buttonResult = UserPromptControl.GetButtonResult(button);

			// Confirm if the user wants to submit this result
			var confirmationResult = ThemedMessageBox.Show($"Are you sure you want to respond '{buttonResult}'?", "Confirmation",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (confirmationResult == MessageBoxResult.Yes) {
				// Find the UserPromptControl that contains the button
				var userPromptControl = FindUserPromptControl(button);
				if (userPromptControl is not null) {
					// Assign the result and the dialog will close
					userPromptControl.Result = buttonResult;
					return;
				}
			}
		}

		// NOTE: By not assigning UserPromptControl.Result, the dialog will remain open
	}

}
