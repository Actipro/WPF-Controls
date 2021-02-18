using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Windows.Controls.Ribbon;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor {

	/// <summary>
	/// Contains the information about the currently open file.
	/// </summary>
	public class DocumentData {

		private bool	modified;
		private string	path;

		internal static	int	NewDocumentCount = 0;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>DocumentData</c> class.
		/// </summary>
		/// <param name="path">The full path to the file.</param>
		public DocumentData(string path) {
			// Initialize parameters
			this.path = path;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a <see cref="DocumentData"/> for a new document.
		/// </summary>
		/// <returns>The <see cref="DocumentData"/> that was created.</returns>
		public static DocumentData CreateNewDocument() {
			return DocumentData.CreateNewDocument(".rtf");
		}

		/// <summary>
		/// Creates a <see cref="DocumentData"/> for a new document.
		/// </summary>
		/// <param name="extension">The extension for the new document.</param>
		/// <returns>The <see cref="DocumentData"/> that was created.</returns>
		public static DocumentData CreateNewDocument(string extension) {
			return new DocumentData("Document" + (++DocumentData.NewDocumentCount) + extension);
		}

		/// <summary>
		/// Gets the filename's extension.
		/// </summary>
		/// <value>The filename's extension.</value>
		public string FilenameExtension {
			get {
				return System.IO.Path.GetExtension(path).ToLower();
			}
		}

		/// <summary>
		/// Gets the filename without an extension.
		/// </summary>
		/// <value>The filename without an extension.</value>
		public string FilenameWithoutExtension {
			get {
				return System.IO.Path.GetFileNameWithoutExtension(path);
			}
		}

		/// <summary>
		/// Gets or sets whether the file has been modified.
		/// </summary>
		/// <value>
		/// <c>true</c> if the file has been modified; otherwise, <c>false</c>.
		/// </value>
		public bool Modified {
			get {
				return modified;
			}
			set {
				modified = value;
			}
		}

		/// <summary>
		/// Gets or sets the full path to the file.
		/// </summary>
		/// <value>The full path to the file.</value>
		public string Path {
			get {
				return path;
			}
			set {
				path = value;
			}
		}

	}
}

