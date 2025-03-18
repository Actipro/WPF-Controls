namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b {
    using ActiproSoftware.Text.Lexing.Implementation;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    
    
    /// <summary>
    /// Contains the token IDs for the <c>Simple</c> language.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v23.1.5.0 (http://www.actiprosoftware.com).
    /// Generated code is based on input created by Actipro Software LLC.
    /// Copyright (c) 2001-2025 Actipro Software LLC.  All rights reserved.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("LanguageDesigner", "23.1.5.0")]
    public partial class SimpleTokenId : TokenIdProviderBase {
        
        /// <summary>
        /// Gets the <c>Whitespace</c> token ID.
        /// </summary>
        public const Int32 Whitespace = 1;
        
        /// <summary>
        /// Gets the <c>OpenParenthesis</c> token ID.
        /// </summary>
        public const Int32 OpenParenthesis = 2;
        
        /// <summary>
        /// Gets the <c>CloseParenthesis</c> token ID.
        /// </summary>
        public const Int32 CloseParenthesis = 3;
        
        /// <summary>
        /// Gets the <c>OpenCurlyBrace</c> token ID.
        /// </summary>
        public const Int32 OpenCurlyBrace = 4;
        
        /// <summary>
        /// Gets the <c>CloseCurlyBrace</c> token ID.
        /// </summary>
        public const Int32 CloseCurlyBrace = 5;
        
        /// <summary>
        /// Gets the <c>Function</c> token ID.
        /// </summary>
        public const Int32 Function = 6;
        
        /// <summary>
        /// Gets the <c>Return</c> token ID.
        /// </summary>
        public const Int32 Return = 7;
        
        /// <summary>
        /// Gets the <c>Var</c> token ID.
        /// </summary>
        public const Int32 Var = 8;
        
        /// <summary>
        /// Gets the <c>Identifier</c> token ID.
        /// </summary>
        public const Int32 Identifier = 9;
        
        /// <summary>
        /// Gets the <c>Equality</c> token ID.
        /// </summary>
        public const Int32 Equality = 10;
        
        /// <summary>
        /// Gets the <c>Inequality</c> token ID.
        /// </summary>
        public const Int32 Inequality = 11;
        
        /// <summary>
        /// Gets the <c>Assignment</c> token ID.
        /// </summary>
        public const Int32 Assignment = 12;
        
        /// <summary>
        /// Gets the <c>Addition</c> token ID.
        /// </summary>
        public const Int32 Addition = 13;
        
        /// <summary>
        /// Gets the <c>Subtraction</c> token ID.
        /// </summary>
        public const Int32 Subtraction = 14;
        
        /// <summary>
        /// Gets the <c>Multiplication</c> token ID.
        /// </summary>
        public const Int32 Multiplication = 15;
        
        /// <summary>
        /// Gets the <c>Division</c> token ID.
        /// </summary>
        public const Int32 Division = 16;
        
        /// <summary>
        /// Gets the <c>Comma</c> token ID.
        /// </summary>
        public const Int32 Comma = 17;
        
        /// <summary>
        /// Gets the <c>SemiColon</c> token ID.
        /// </summary>
        public const Int32 SemiColon = 18;
        
        /// <summary>
        /// Gets the <c>Number</c> token ID.
        /// </summary>
        public const Int32 Number = 19;
        
        /// <summary>
        /// Gets the <c>SingleLineCommentText</c> token ID.
        /// </summary>
        public const Int32 SingleLineCommentText = 20;
        
        /// <summary>
        /// Gets the <c>SingleLineCommentStartDelimiter</c> token ID.
        /// </summary>
        public const Int32 SingleLineCommentStartDelimiter = 21;
        
        /// <summary>
        /// Gets the <c>SingleLineCommentEndDelimiter</c> token ID.
        /// </summary>
        public const Int32 SingleLineCommentEndDelimiter = 22;
        
        /// <summary>
        /// Gets the <c>MultiLineCommentText</c> token ID.
        /// </summary>
        public const Int32 MultiLineCommentText = 23;
        
        /// <summary>
        /// Gets the <c>MultiLineCommentStartDelimiter</c> token ID.
        /// </summary>
        public const Int32 MultiLineCommentStartDelimiter = 24;
        
        /// <summary>
        /// Gets the <c>MultiLineCommentEndDelimiter</c> token ID.
        /// </summary>
        public const Int32 MultiLineCommentEndDelimiter = 25;
        
        /// <summary>
        /// Gets the <c>MultiLineCommentLineTerminator</c> token ID.
        /// </summary>
        public const Int32 MultiLineCommentLineTerminator = 26;
        
        /// <summary>
        /// Gets the minimum token ID returned by this provider.
        /// </summary>
        /// <value>The minimum token ID returned by this provider.</value>
        public override Int32 MinId {
            get {
                return 1;
            }
        }
        
        /// <summary>
        /// Gets the maximum token ID returned by this provider.
        /// </summary>
        /// <value>The maximum token ID returned by this provider.</value>
        public override Int32 MaxId {
            get {
                return 26;
            }
        }
        
        /// <summary>
        /// Returns whether the specified ID value is valid for this token ID provider.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns><c>true</c> if the ID value is valid; otherwise, <c>false</c></returns>
        public override Boolean ContainsId(Int32 id) {
            return ((id >= MinId) 
                        && (id <= MaxId));
        }
        
        /// <summary>
        /// Returns the public static fields in this ID provider.
        /// </summary>
        /// <returns>The public static fields in this ID provider.</returns>
        private static FieldInfo[] GetFields() {
			#if WINRT
			return typeof(SimpleTokenId).GetTypeInfo().DeclaredFields.Where(f => (f.IsPublic) && (f.IsStatic)).ToArray();
			#else
			return typeof(SimpleTokenId).GetFields((BindingFlags.Public | BindingFlags.Static));
			#endif
        }
        
        /// <summary>
        /// Returns the actual string representation for the specified token ID.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns>The actual string representation for the specified token ID.</returns>
        public override String GetDescription(Int32 id) {
            FieldInfo[] fields = GetFields();
            for (Int32 index = 0; (index < fields.Length); index = (index + 1)) {
                FieldInfo field = fields[index];
                if (id.Equals(field.GetValue(null))) {
                    Object descriptionAttr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                    if ((descriptionAttr != null)) {
                        return ((DescriptionAttribute)(descriptionAttr)).Description;
                    }
                    else {
                        return field.Name;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Returns the string-based key for the specified token ID.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns>The string-based key for the specified token ID.</returns>
        public override String GetKey(Int32 id) {
            FieldInfo[] fields = GetFields();
            for (Int32 index = 0; (index < fields.Length); index = (index + 1)) {
                FieldInfo field = fields[index];
                if (id.Equals(field.GetValue(null))) {
                    return field.Name;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Returns whether the specified ID value is part of the <c>Comment</c>.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns><c>true</c> if the ID value is in the classification type; otherwise, <c>false</c></returns>
        public static Boolean IsCommentClassificationType(Int32 id) {
            return ((id == SingleLineCommentText) 
                        || (id == MultiLineCommentText));
        }
        
        /// <summary>
        /// Returns whether the specified ID value is part of the <c>Delimiter</c>.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns><c>true</c> if the ID value is in the classification type; otherwise, <c>false</c></returns>
        public static Boolean IsDelimiterClassificationType(Int32 id) {
            return ((id >= OpenParenthesis) 
                        && (id <= CloseCurlyBrace));
        }
        
        /// <summary>
        /// Returns whether the specified ID value is part of the <c>Identifier</c>.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns><c>true</c> if the ID value is in the classification type; otherwise, <c>false</c></returns>
        public static Boolean IsIdentifierClassificationType(Int32 id) {
            return (id == Identifier);
        }
        
        /// <summary>
        /// Returns whether the specified ID value is part of the <c>Keyword</c>.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns><c>true</c> if the ID value is in the classification type; otherwise, <c>false</c></returns>
        public static Boolean IsKeywordClassificationType(Int32 id) {
            return ((id >= Function) 
                        && (id <= Var));
        }
        
        /// <summary>
        /// Returns whether the specified ID value is part of the <c>Number</c>.
        /// </summary>
        /// <param name="id">The token ID to examine.</param>
        /// <returns><c>true</c> if the ID value is in the classification type; otherwise, <c>false</c></returns>
        public static Boolean IsNumberClassificationType(Int32 id) {
            return (id == Number);
        }
    }
}
