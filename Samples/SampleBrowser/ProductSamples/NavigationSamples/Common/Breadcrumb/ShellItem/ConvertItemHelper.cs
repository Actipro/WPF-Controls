using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Xml;

using ActiproSoftware.Windows.Controls.Navigation;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem {
	/// <summary>
	/// This class includes helper methods for working with the Breadcrumb ConvertItem event.
	/// </summary>
	public static class ConvertItemHelper {
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the path entry for the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		private static string GetPathEntry(XmlElement element) {
			if (null == element || 0 == element.Attributes.Count)
				return string.Empty;
			return element.Attributes["Name"].Value;
		}

		/// <summary>
		/// Reports an error to the user.
		/// </summary>
		/// <param name="text">The text.</param>
		private static void ReportError(string text) {
			MessageBox.Show(text, "Breadcrumb Sample", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the item from the specified path.
		/// </summary>
		/// <param name="rootItem">The root item.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static object GetItem(object rootItem, string path) {
			IList trail = GetTrail(rootItem, path);
			if (null == trail || 0 == trail.Count)
				return null;
			return trail[trail.Count - 1];
		}

		/// <summary>
		/// Gets the path for the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public static string GetPath(object item) {
			XmlElement element = item as XmlElement;
			if (null == element)
				return null;

			StringBuilder sb = new StringBuilder();
			sb.Append(GetPathEntry(element));

			XmlNode node = element.ParentNode;
			while (null != node && node.NodeType == XmlNodeType.Element) {
				sb.Insert(0, '\\');
				sb.Insert(0, GetPathEntry(node as XmlElement));
				node = node.ParentNode;
			}
			return sb.ToString();
		}

		/// <summary>
		/// Gets the trail for the specified item.
		/// </summary>
		/// <param name="rootItem">The root item.</param>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public static IList GetTrail(object rootItem, object item) {
			// If the is an XmlElement, then we can get the location.
			XmlElement element = item as XmlElement;
			if (null == element)
				return null;

			// Start to build the trail
			List<XmlElement> trail = new List<XmlElement>();
			do {
				// Add the current element to the trail
				trail.Insert(0, element);

				if (object.ReferenceEquals(element, rootItem))
					return trail;

				// Get the parent element of the current element
				element = element.ParentNode as XmlElement;

			} while (null != element);

			// We never found the root item, so the given item must not be a descendant
			return null;
		}

		/// <summary>
		/// Gets the trail for the specified path.
		/// </summary>
		/// <param name="rootItem">The root item.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static IList GetTrail(object rootItem, string path) {
			// Make sure the specified path is valid
			if (string.IsNullOrEmpty(path))
				return null;

			// If the root element was not passed, then we cannot build a trail
			XmlElement element = rootItem as XmlElement;
			if (null == element)
				return null;

			// Break the path up based on the specified path separator
			string[] pathEntries = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
			if (null == pathEntries || 0 == pathEntries.Length)
				return null;

			// The root element need to be the first path entry, so we will do that comparison first
			string pathEntry = GetPathEntry(element);
			if (0 == string.Compare(pathEntry, pathEntries[0], StringComparison.CurrentCultureIgnoreCase)) {
				// The root element matched, so we can continue to build the trail
				List<XmlElement> trail = new List<XmlElement>(pathEntries.Length);
				trail.Add(element);

				// For the remaining entries in the path, we will search the child nodes for a match at each level. If at any
				//   point we don't find a match, then we will need to cancel the conversion.
				for (int index = 1; index < pathEntries.Length; index++) {
					// Get the first child node and loop through it's siblings until we find a match for the current path
					//   entry
					XmlNode child = element.FirstChild;
					while (null != child) {
						XmlElement childElement = child as XmlElement;
						if (null != childElement) {
							pathEntry = GetPathEntry(childElement);
							if (0 == string.Compare(pathEntry, pathEntries[index], StringComparison.CurrentCultureIgnoreCase))
								break; // Found a match
						}

						// We didn't find a match, so continue with the next sibling (if any)
						child = child.NextSibling;
					}

					// The child variable will now point to the next element in the trail, or to null which indicates at match
					//   was not found.
					element = child as XmlElement;
					if (null != element)
						trail.Add(element);
					else
						return null;
				}

				return trail;
			}

			return null;
		}

		/// <summary>
		/// Handles the ConvertItem event of a Breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		public static void HandleConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			if (BreadcrumbConvertItemTargetType.Path == e.TargetType) {
				// Convert either the item or the trail to a path
				object item = e.Item;
				if (null == item && null != e.Trail && 0 != e.Trail.Count)
					item = e.Trail[e.Trail.Count - 1];

				e.Path = GetPath(item);
			}
			else if (BreadcrumbConvertItemTargetType.Trail == e.TargetType) {
				IList trail = null;
				if (null != e.Path)
					trail = GetTrail(e.RootItem, e.Path);
				else if (null != e.Item)
					trail = GetTrail(e.RootItem, e.Item);

				if (null == trail) {
					ReportError("The specified path could not be found.");
					return;
				}

				e.Trail = trail;
			}
			else {
				throw new NotImplementedException("Unsupported Breadcrumb target type");
			}
		}
	}
}
