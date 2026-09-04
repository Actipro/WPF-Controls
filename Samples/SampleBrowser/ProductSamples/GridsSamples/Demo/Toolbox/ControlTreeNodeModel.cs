using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Input;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides a tree node model implementation for a toolbox control.
/// </summary>
public class ControlTreeNodeModel : ToolboxTreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="data">The control data to be represented by the model.</param>
	public ControlTreeNodeModel(ControlData data) {
		Data = data ?? throw new ArgumentNullException(nameof(data));
		Name = GetControlNameOnly(data.FullName);
		ImageSource = new BitmapImage(new Uri($"/Images/Icons/Toolbox{Category}{Name}16.png", UriKind.Relative));

		ToggleFavoriteCommand = new DelegateCommand<object>(_ => {
			IsFavorite = !IsFavorite;
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The data used by the model.
	/// </summary>
	private ControlData Data { get; }

	/// <summary>
	/// Returns only the name of the control from the full name.
	/// </summary>
	/// <param name="fullName">The full name of the control.</param>
	/// <returns>The name of the control.</returns>
	private static string GetControlNameOnly(string fullName) {
		// Full name includes the namespace, so the last part of the full name is the control
		return fullName.Split('.').Last();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The category for the control.
	/// </summary>
	public string Category
		=> Data.Category;

	/// <inheritdoc/>
	public override string DataObjectText {
		// Use the full control name as the default text for drag operations
		get => FullName;
	}

	/// <inheritdoc/>
	protected override bool DefaultIsDraggable {
		// Allow controls to be dragged
		get => true;
	}

	/// <summary>
	/// The full name of the control.
	/// </summary>
	public string FullName
		=> Data.FullName;

	/// <summary>
	/// Indicates whether the control is a favorite.
	/// </summary>
	public bool IsFavorite {
		get => ControlDataRepository.Instance.IsFavorite(Data);
		set {
			if (IsFavorite != value) {
				if (value)
					ControlDataRepository.Instance.AddFavorite(Data);
				else
					ControlDataRepository.Instance.RemoveFavorite(Data);

				OnPropertyChanged();
			}
		}
	}

	/// <summary>
	/// The <see cref="ICommand"/> that can be used to toggle if the control is a favorite.
	/// </summary>
	public ICommand ToggleFavoriteCommand { get; }

}
