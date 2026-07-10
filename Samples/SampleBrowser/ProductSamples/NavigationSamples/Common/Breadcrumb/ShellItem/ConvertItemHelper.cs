using ActiproSoftware.Windows.Controls.Navigation;
using System.Xml;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

/// <summary>
/// This class includes helper methods for working with the Breadcrumb ConvertItem event.
/// </summary>
public static class ConvertItemHelper {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the path entry for the specified element.
	/// </summary>
	/// <param name="element">The element.</param>
	private static string GetPathEntry(XmlElement? element) {
		return (element is { Attributes.Count: > 0 })
			? element.Attributes["Name"]?.Value ?? string.Empty
			: string.Empty;
	}

	/// <summary>
	/// Reports an error to the user.
	/// </summary>
	/// <param name="text">The text.</param>
	private static void ReportError(string text)
		=> MessageBox.Show(text, "Breadcrumb Sample", MessageBoxButton.OK, MessageBoxImage.Error);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the item from the specified path.
	/// </summary>
	/// <param name="rootItem">The root item.</param>
	/// <param name="path">The path.</param>
	public static object? GetItem(object? rootItem, string path) {
		var trail = GetTrail(rootItem, path);
		return (trail is { Count: > 0 })
			? trail[trail.Count - 1]
			: null;
	}

	/// <summary>
	/// Returns the path for the specified item.
	/// </summary>
	/// <param name="item">The item.</param>
	public static string? GetPath(object? item) {
		if (item is not XmlElement element)
			return null;

		var sb = new StringBuilder();
		sb.Append(GetPathEntry(element));

		var node = element.ParentNode;
		while (node?.NodeType == XmlNodeType.Element) {
			sb.Insert(0, '\\');
			sb.Insert(0, GetPathEntry(node as XmlElement));
			node = node.ParentNode;
		}
		return sb.ToString();
	}

	/// <summary>
	/// Returns the trail for the specified item.
	/// </summary>
	/// <param name="rootItem">The root item.</param>
	/// <param name="item">The item.</param>
	public static IList? GetTrail(object? rootItem, object? item) {
		// If the is an XmlElement, then we can get the location.
		var element = item as XmlElement;
		if (element is null)
			return null;

		// Start to build the trail
		var trail = new List<XmlElement>();
		do {
			// Add the current element to the trail
			trail.Insert(0, element);

			if (ReferenceEquals(element, rootItem))
				return trail;

			// Get the parent element of the current element
			element = element.ParentNode as XmlElement;

		} while (element is not null);

		// We never found the root item, so the given item must not be a descendant
		return null;
	}

	/// <summary>
	/// Returns the trail for the specified path.
	/// </summary>
	/// <param name="rootItem">The root item.</param>
	/// <param name="path">The path.</param>
	public static IList? GetTrail(object? rootItem, string? path) {
		// Make sure the specified path is valid
		if (string.IsNullOrEmpty(path))
			return null;

		// If the root element was not passed, then we cannot build a trail
		var element = rootItem as XmlElement;
		if (element is null)
			return null;

		// Break the path up based on the specified path separator
		var pathEntries = path!.Split(['\\'], StringSplitOptions.RemoveEmptyEntries);
		if (pathEntries is not { Length: > 0 })
			return null;

		// The root element need to be the first path entry, so we will do that comparison first
		var pathEntry = GetPathEntry(element);
		if (string.Compare(pathEntry, pathEntries[0], StringComparison.CurrentCultureIgnoreCase) == 0) {
			// The root element matched, so we can continue to build the trail
			var trail = new List<XmlElement>(pathEntries.Length) {
				element
			};

			// For the remaining entries in the path, we will search the child nodes for a match at each level. If at any
			//   point we don't find a match, then we will need to cancel the conversion.
			for (var index = 1; index < pathEntries.Length; index++) {
				// Get the first child node and loop through it's siblings until we find a match for the current path entry
				var child = element.FirstChild;
				while (child is not null) {
					if (child is XmlElement childElement) {
						pathEntry = GetPathEntry(childElement);
						if (string.Compare(pathEntry, pathEntries[index], StringComparison.CurrentCultureIgnoreCase) == 0)
							break; // Found a match
					}

					// We didn't find a match, so continue with the next sibling (if any)
					child = child.NextSibling;
				}

				// The child variable will now point to the next element in the trail, or to null which indicates at match was not found.
				element = child as XmlElement;
				if (element is null)
					return null;

				trail.Add(element);
			}

			return trail;
		}

		return null;
	}

	/// <summary>
	/// Handles the <see cref="Windows.Controls.Navigation.Breadcrumb.ConvertItem"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public static void HandleConvertItem(object? sender, BreadcrumbConvertItemEventArgs e) {
		switch (e.TargetType) {
			case BreadcrumbConvertItemTargetType.Path:
				// Convert either the item or the trail to a path
				var item = e.Item;
				if ((item is null) && (e.Trail is { Count: > 0 }))
					item = e.Trail[e.Trail.Count - 1];

				e.Path = GetPath(item);
				break;
			case BreadcrumbConvertItemTargetType.Trail:
				IList? trail = null;
				if (e.Path is not null)
					trail = GetTrail(e.RootItem, e.Path);
				else if (e.Item is not null)
					trail = GetTrail(e.RootItem, e.Item);

				if (trail is null) {
					ReportError("The specified path could not be found.");
					return;
				}

				e.Trail = trail;
				break;
			default:
				throw new NotImplementedException("Unsupported Breadcrumb target type");
		}
	}

}
