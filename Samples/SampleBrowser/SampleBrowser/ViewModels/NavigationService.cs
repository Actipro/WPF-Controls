using ActiproSoftware.Extensions;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Implements a navigation service for samples.
/// </summary>
public class NavigationService {

	private const int MaxHistoryCount = 100;

	private readonly List<ProductItemInfo?> _history = [];
	private int _historyIndex = -1;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether a navigation backward can occur.
	/// </summary>
	public bool CanGoBack
		=> _historyIndex > 0;

	/// <summary>
	/// Indicates whether a navigation forward can occur.
	/// </summary>
	public bool CanGoForward
		=> _historyIndex.IsBetween(0, _history.Count - 2);

	/// <summary>
	/// Navigates backward in history.
	/// </summary>
	/// <returns>The <see cref="ProductItemInfo"/> to navigate to.</returns>
	public ProductItemInfo? GoBack()
		=> CanGoBack ? _history[--_historyIndex] : null;

	/// <summary>
	/// Navigates forward in history.
	/// </summary>
	/// <returns>The <see cref="ProductItemInfo"/> to navigate to.</returns>
	public ProductItemInfo? GoForward()
		=> CanGoForward ? _history[++_historyIndex] : null;

	/// <summary>
	/// Indicates whether a navigation through history is currently occurring.
	/// </summary>
	public bool IsNavigatingThroughHistory { get; set; }

	/// <summary>
	/// Navigates to the specified <see cref="ProductItemInfo"/>.
	/// </summary>
	/// <param name="itemInfo">The <see cref="ProductItemInfo"/> to navigate to.</param>
	public void NavigateTo(ProductItemInfo? itemInfo) {
		if (IsNavigatingThroughHistory)
			return;

		_historyIndex++;
		_history.RemoveRange(_historyIndex, _history.Count - _historyIndex);
		_history.Add(itemInfo);

		if (_history.Count > MaxHistoryCount) {
			_historyIndex--;
			_history.RemoveAt(0);
		}
	}

}
