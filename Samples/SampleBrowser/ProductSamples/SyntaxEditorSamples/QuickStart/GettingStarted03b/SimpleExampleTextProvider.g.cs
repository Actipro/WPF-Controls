namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b {
    using ActiproSoftware.Text;
    using System;
    
    
    /// <summary>
    /// Represents a example text provider for the <c>Simple</c> language.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v23.1.5.0 (http://www.actiprosoftware.com).
    /// Generated code is based on input created by Actipro Software LLC.
    /// Copyright (c) 2001-2025 Actipro Software LLC.  All rights reserved.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("LanguageDesigner", "23.1.5.0")]
    public partial class SimpleExampleTextProvider : Object, IExampleTextProvider {
        
        /// <summary>
        /// Gets the example text, a code snippet of the related language.
        /// </summary>
        /// <value>The example text, a code snippet of the related language.</value>
        public String ExampleText {
            get {
                return @"/*
	Simple Language
*/

function Add(x, y) {
	return x + y;
}

function Increment(x) {
	return (x + 1);
}

function IncrementAndMultiply(x, y) {
	// This function calls another function
	var result;
	result = Increment(x);
	return result * y;
}
";
            }
        }
    }
}
