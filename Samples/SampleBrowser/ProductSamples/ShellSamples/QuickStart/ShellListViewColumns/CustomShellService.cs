using System;
using System.Collections.Generic;
using ActiproSoftware.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellListViewColumns {
	
	/// <summary>
	/// Provides an <see cref="IShellService"/> implementation that can inject custom shell objects.
	/// </summary>
	public class CustomShellService : WindowsShellService {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		public override ShellPropertyCollection CreateProperties(IList<IShellObject> shellObjects, ShellPropertyRequestKind kind) {
			var props = base.CreateProperties(shellObjects, kind);
			
			// Insert the custom shell property and render it in the second column
			props.Insert(1, new CustomShellProperty());
			
			return props;
		}

		public override object GetPropertyValue(IShellObject shellObject, IShellProperty shellProperty) {
			var propValue = base.GetPropertyValue(shellObject, shellProperty);
			
			if ((propValue == null) && (shellProperty is CustomShellProperty)) {
				// Return the value for the custom shell property
				propValue = shellObject.Kind;
			}

			return propValue;
		}

	}

}