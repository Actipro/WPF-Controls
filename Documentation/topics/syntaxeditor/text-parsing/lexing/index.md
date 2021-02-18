---
title: "Overview"
page-title: "Lexing - SyntaxEditor Text/Parsing Framework"
order: 1
---
# Overview

Lexing is the process of scanning text and breaking it up into meaningful tokens that can be examined by a higher-level parser.

## Basic Concepts

There are various types of lexers and ways to go in terms of implementing them.  This topic walks through how a lexer works and the benefits/drawbacks of various implementations.

See the [Basic Concepts](basic-concepts.md) topic for more information.

## Tokens

A token is a span of text within a snapshot that has some sort of lexical parse data associated with it.  Tokens are used throughout the product, in various types of parsing, classification, and text scanning.

See the [Tokens](tokens.md) topic for more information.

## Dynamic Lexers

Dynamic lexers are mergable lexers and use our custom pattern-based regular expression engine to match and tokenize text.  It is recommended that developers new to the text/parsing framework as well as developers who do not require much advanced parsing use dynamic lexers since they make it simple to get up and running quickly.

See the [Dynamic Lexers](dynamic-lexers.md) topic for more information.

## Programmatic Lexers

The fastest lexers are handwritten classes (called "programmatic") that are hard-coded to parse a specific language's code.

See the [Programmatic Lexers](programmatic-lexers.md) topic for more information.
