using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly Dictionary<string, ControlTreeNodeModel> _controlModels = [];
	private FavoritesCategoryTreeNodeModel? _favoritesModel;
	private readonly CategoryTreeNodeModel _rootModel;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Create the root model that will be used to populate the toolbox
		_rootModel = new CategoryTreeNodeModel() {
			Name = "Toolbox",
			IsExpanded = true
		};
		treeListBox.RootItem = _rootModel;

		// Initialize the data
		ResetToolbox();

		// Listen for changes in Favorites
		ControlDataRepository.Instance.FavoritesChanged += OnControlDataRepositoryFavoritesChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a model to represent a category in the toolbox.
	/// </summary>
	/// <param name="categoryName">The category name.</param>
	private static CategoryTreeNodeModel CreateCategoryModel(string categoryName)
		=> new() { Name = categoryName, IsExpanded = true };

	/// <summary>
	/// Creates a model to represent a control in the toolbox.
	/// </summary>
	/// <param name="controlData">The data of the control to be modeled.</param>
	private ControlTreeNodeModel CreateControlModel(ControlData controlData) {
		var controlModel = new ControlTreeNodeModel(controlData);
		_controlModels[controlData.FullName] = controlModel;
		return controlModel;
	}

	/// <summary>
	/// Attempts to gets an existing model representing the category in the toolbox. If a model
	/// does not exist for the category, one will be created and added to the toolbox.
	/// </summary>
	/// <param name="categoryName">The name of the category.</param>
	/// <returns>Returns the existing model if found; otherwise returns the model that was created for the category.</returns>
	private CategoryTreeNodeModel GetCategoryModel(string categoryName) {
		// Attempt to find an existing category model
		var categoryModel = _rootModel
			.Children
			.OfType<CategoryTreeNodeModel>()
			.FirstOrDefault(m => m.Name == categoryName);

		// If not found, create one
		if (categoryModel is null) {
			categoryModel = CreateCategoryModel(categoryName);
			_rootModel.Children.Add(categoryModel);
		}

		return categoryModel;
	}

	/// <summary>
	/// This method is called when the <see cref="ControlDataRepository.Favorites"/> are changed.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event args.</param>
	private void OnControlDataRepositoryFavoritesChanged(object? sender, CollectionChangeEventArgs e) {
		// Quit if the favorites model is not defined
		if (_favoritesModel is null)
			return;

		switch (e.Action) {

			case CollectionChangeAction.Refresh: {
				// Clear all favorites
				_favoritesModel.Children.Clear();

				// Make sure each model is reset
				foreach (var controlModel in _controlModels.Values)
					controlModel.IsFavorite = false;

				break;
			}

			case CollectionChangeAction.Add: {
				if (e.Element is ControlData controlData) {
					// Add the the control to the favorites category
					if (FindFavoriteControlModel(controlData) is null)
						_favoritesModel.Children.Add(CreateControlModel(controlData));

					// Make sure the model is marked as a favorite
					if (_controlModels.TryGetValue(controlData.FullName, out var controlModel))
						controlModel.IsFavorite = true;
				}
				break;
			}

			case CollectionChangeAction.Remove: {
				if (e.Element is ControlData controlData) {
					// Remove existing favorite
					var favoriteControlModel = FindFavoriteControlModel(controlData);
					if (favoriteControlModel is not null)
						_favoritesModel.Children.Remove(favoriteControlModel);

					// Make sure the model is not marked as a favorite
					if (_controlModels.TryGetValue(controlData.FullName, out var controlModel))
						controlModel.IsFavorite = false;
				}
				break;
			}
		}

		// Finds the model, if any, for a control represented in the Favorites category
		ControlTreeNodeModel? FindFavoriteControlModel(ControlData controlData) {
			return _favoritesModel
				.Children
				.OfType<ControlTreeNodeModel>()
				.FirstOrDefault(m => m.FullName == controlData.FullName);
		}
	}

	/// <summary>
	/// This method is called when a drag operation completes by dropping over the simulated designer surface.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event args.</param>
	private void OnDesignerDrop(object sender, DragEventArgs e) {
		// Lookup the dropped control (only a single item is supported)
		var controlModel = CustomTreeListBoxItemAdapter
			.GetDraggedModels(e.Data, treeListBox)
			.OfType<ControlTreeNodeModel>()
			.FirstOrDefault();

		// Here is where the dropped item would be processed. For this demo, only a message will be displayed.
		if (controlModel is null)
			MessageBox.Show($"The dropped item is not supported by this designer.", "UI Designer", MessageBoxButton.OK, MessageBoxImage.Warning);
		else
			MessageBox.Show($"Add the following control to the designer:\r\n\r\n{controlModel.FullName}", "UI Designer", MessageBoxButton.OK, MessageBoxImage.Information);
	}

	/// <summary>
	/// This method is called when a contextual menu is requested for a list box item.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event args.</param>
	private void OnToolboxItemMenuRequested(object sender, TreeListBoxItemMenuEventArgs e) {
		// Add model-specific menu items
		if (e.Item is ControlTreeNodeModel controlModel) {
			StartNewMenuGroup();
			e.Menu!.Items.Add(new MenuItem() {
				Header = controlModel.IsFavorite ? "Remove from Favorites" : "Add to Favorites",
				Command = controlModel.ToggleFavoriteCommand
			});
		}
		else if (e.Item is FavoritesCategoryTreeNodeModel) {
			StartNewMenuGroup();
			e.Menu!.Items.Add(new MenuItem() {
				Header = "Clear Favorites",
				Command = new DelegateCommand<object>(_ => ControlDataRepository.Instance.ClearFavorites())
			});
		}

		// Add common menu items
		StartNewMenuGroup();
		e.Menu!.Items.Add(new MenuItem() {
			Header = "Reset Toolbox",
			Command = new DelegateCommand<object>(_ => ResetToolbox())
		});

		// Call this method to prepare a menu for a new group of menu items
		void StartNewMenuGroup() {
			if (e.Menu is null)
				e.Menu = new ContextMenu();
			else
				e.Menu.Items.Add(new Separator());
		}
	}

	/// <summary>
	/// Resets the data used to populate the toolbox.
	/// </summary>
	private void ResetToolbox() {
		// Reset the repository
		ControlDataRepository.Instance.Reset();

		// Clear all cached models for controls
		_controlModels.Clear();

		// Remove all nodes from the toolbox
		_rootModel.Children.Clear();

		// Create the Favorites category and add to the toolbox
		_favoritesModel = new FavoritesCategoryTreeNodeModel() { IsExpanded = true };
		_rootModel.Children.Add(_favoritesModel);

		// Iterate the category of all defined controls
		foreach (var categoryName in ControlDataRepository.Instance.DistinctCategories) {

			// Get the model for the category. If one does not exist, it will be created and added to the toolbox.
			var categoryModel = GetCategoryModel(categoryName);

			// Add a model for each control that belongs to this category.
			foreach (var controlData in ControlDataRepository.Instance.FindByCategory(categoryName)) {
				var controlModel = CreateControlModel(controlData);
				categoryModel.Children.Add(controlModel);
			}
		}

	}

}
