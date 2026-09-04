namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides a repository for available <see cref="ControlData"/> instances.
/// </summary>
class ControlDataRepository {

	public static readonly ControlDataRepository Instance = new();

	private readonly Dictionary<string, ControlData> _store = [];
	private readonly HashSet<string> _favorites = [];

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	public event EventHandler<CollectionChangeEventArgs>? FavoritesChanged;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	private ControlDataRepository() {
		Reset();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds <c>ControlData</c> to the repository.
	/// </summary>
	/// <param name="controlData">The <c>ControlData</c> to add.</param>
	private void Add(ControlData controlData)
		=> _store[controlData.FullName] = controlData;

	/// <summary>
	/// Adds a range of <c>ControlData</c> to the repository.
	/// </summary>
	/// <param name="range">The range of <c>ControlData</c> to add.</param>
	private void AddRange(IEnumerable<ControlData> range) {
		foreach (var controlData in range)
			Add(controlData);
	}

	/// <summary>
	/// Creates a new instance of <see cref="ControlData"/> for a control of the given type.
	/// </summary>
	/// <param name="controlType">The <see cref="Type"/> of the control.</param>
	private static ControlData CreateControlData(Type controlType) {
		if (controlType is not { Namespace: not null, FullName: not null })
			throw new ArgumentException("The type must define a Namespace and FullName.");

		// Use the last part of the namespace as the category
		string category = controlType.Namespace.Split('.').Last();
		return new ControlData(controlType.FullName, category);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds the control data as a favorite.
	/// </summary>
	/// <param name="controlData">The control data.</param>
	/// <returns><c>true</c> if the control data was added as a favorite; otherwise <c>false</c> if it was already a favorite and no action was necessary.</returns>
	public bool AddFavorite(ControlData controlData) {
		if (_favorites.Add(controlData.FullName)) {
			FavoritesChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, controlData));
			return true;
		}
		return false;
	}

	/// <summary>
	/// Removes the control data as a favorite.
	/// </summary>
	/// <param name="controlData">The control data.</param>
	/// <returns><c>true</c> if the control data was removed as a favorite; otherwise <c>false</c> if it was not a favorite and no action was necessary.</returns>
	public bool RemoveFavorite(ControlData controlData) {
		if (_favorites.Remove(controlData.FullName)) {
			FavoritesChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, controlData));
			return true;
		}
		return false;
	}

	/// <summary>
	/// Clears all favorites.
	/// </summary>
	public void ClearFavorites() {
		_favorites.Clear();
		FavoritesChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, element: null));
	}

	/// <summary>
	/// An enumerable of the data for all controls in the repository.
	/// </summary>
	public IEnumerable<ControlData> Controls
		=> _store.Values;

	/// <summary>
	/// An enumerable of distinct category names for controls in the repository.
	/// </summary>
	public IEnumerable<string> DistinctCategories
		=> _store.Values.Select(cd => cd.Category).Distinct();

	/// <summary>
	/// An enumerable of all controls in the repository which have been designated as favorites.
	/// </summary>
	public IEnumerable<ControlData> Favorites {
		get {
			// Iterate the names of all controls marked as favorites
			foreach (var fullName in _favorites) {
				if (Find(fullName) is { } controlData)
					yield return controlData;
			}
		}
	}

	/// <summary>
	/// Attempts to find data in the repository for the specified full name of the control.
	/// </summary>
	/// <param name="fullName">The full name of the control.</param>
	public ControlData? Find(string fullName) {
		if (_store.TryGetValue(fullName, out var controlData))
			return controlData;
		return null;
	}

	/// <summary>
	/// Returns an enumerable of all controls in the repository for the given <paramref name="category"/>. Category names are not case-sensitive.
	/// </summary>
	/// <param name="category">The name of the category.</param>
	public IEnumerable<ControlData> FindByCategory(string category)
		=> Controls.Where(cd => string.Compare(cd.Category, category, StringComparison.InvariantCultureIgnoreCase) == 0);

	/// <summary>
	/// Tests if the specified <c>ControlData</c> is designated as a favorite.
	/// </summary>
	/// <param name="controlData">The control data to test.</param>
	/// <returns><c>true</c> if the <c>ControlData</c> is a favorite; otherwise <c>false</c>.</returns>
	public bool IsFavorite(ControlData controlData)
		=> _favorites.Contains(controlData.FullName);

	/// <summary>
	/// Resets the repository to the initial state.
	/// </summary>
	public void Reset() {
		ClearFavorites();
		_store.Clear();

		AddRange([
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.Docking.AdvancedTabControl)),
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.Docking.DockSite)),
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.Docking.WindowControl)),
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.Editors.AutoCompleteBox)),
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.SyntaxEditor.NavigableSymbolSelector)),
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor)),
			CreateControlData(typeof(ActiproSoftware.Windows.Controls.SyntaxEditor.TextStylePreview)),
		]);

	}

}
