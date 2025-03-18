namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d {
    using ActiproSoftware.Text.Parsing;
    using System;
    using System.Collections.Generic;
    
    
    /// <summary>
    /// Represents a parenthesized expression.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v11.1.544.0 (http://www.actiprosoftware.com).
    /// Generated code is based on input created by Actipro Software LLC.
    /// Copyright (c) 2001-2025 Actipro Software LLC.  All rights reserved.
    /// </remarks>
    public partial class ParenthesizedExpression : Expression {
        
        /// <summary>
        /// Gets the child expression.
        /// </summary>
        private Expression childExpressionValue;
        
        /// <summary>
        /// Gets the An integer value that identifies the type of AST node.
        /// </summary>
        /// <value>The An integer value that identifies the type of AST node.</value>
        public override Int32 Id {
            get {
                return SimpleAstNodeId.ParenthesizedExpression;
            }
        }
        
        /// <summary>
        /// Gets or sets the child expression.
        /// </summary>
        /// <value>The child expression.</value>
        public Expression ChildExpression {
            get {
                return this.childExpressionValue;
            }
            set {
                this.childExpressionValue = value;
            }
        }
        
        /// <summary>
        /// Retrieves an <c>IEnumerator</c> object that can iterate the child <see cref="IAstNode"/> objects in this node.
        /// </summary>
        /// <returns>An <c>IEnumerator</c> object that can iterate the child <see cref="IAstNode"/> objects in this node.</returns>
        protected override IEnumerator<IAstNode> GetChildrenEnumerator() {
            IEnumerator<IAstNode> baseEnumerator = base.GetChildrenEnumerator();
            if ((baseEnumerator != null)) {
				while (baseEnumerator.MoveNext())
					yield return baseEnumerator.Current;
            }
            if ((this.childExpressionValue != null)) {
				yield return this.childExpressionValue;
            }
        }
    }
}
