namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d {
    using ActiproSoftware.Text.Parsing;
    using System;
    using System.Collections.Generic;
    
    
    /// <summary>
    /// Represents an assignment statement.
    /// </summary>
    /// <remarks>
    /// This type was generated by the Actipro Language Designer tool v11.1.544.0 (http://www.actiprosoftware.com).
    /// Generated code is based on input created by Actipro Software LLC.
    /// Copyright (c) 2001-2025 Actipro Software LLC.  All rights reserved.
    /// </remarks>
    public partial class AssignmentStatement : Statement {
        
        /// <summary>
        /// Gets the expression.
        /// </summary>
        private Expression expressionValue;
        
        /// <summary>
        /// Gets the variable name.
        /// </summary>
        private String variableNameValue;
        
        /// <summary>
        /// Gets the An integer value that identifies the type of AST node.
        /// </summary>
        /// <value>The An integer value that identifies the type of AST node.</value>
        public override Int32 Id {
            get {
                return SimpleAstNodeId.AssignmentStatement;
            }
        }
        
        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public Expression Expression {
            get {
                return this.expressionValue;
            }
            set {
                this.expressionValue = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the variable name.
        /// </summary>
        /// <value>The variable name.</value>
        public String VariableName {
            get {
                return this.variableNameValue;
            }
            set {
                this.variableNameValue = value;
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
            if ((this.expressionValue != null)) {
				yield return this.expressionValue;
            }
        }
    }
}
