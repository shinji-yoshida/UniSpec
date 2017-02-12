using System.Collections.Generic;
using NUnit.Framework;
using System;
using UnityEngine;

namespace UniSpec {
	public class SpecSuite {
		Stack<Context> contexts;

		[SetUp]
		public void SetUp() {
			contexts = new Stack<Context> ();
		}

		protected void Describe(string message, Action descriptionBuilder) {
			Debug.Assert (contexts.Count == 0, "'Describe' should be at root");

			var execContext = new ExecutionContext ();

			var description = new Context(message);

			contexts.Push (description);
			descriptionBuilder ();
			contexts.Pop ();

			description.Execute (execContext);
		}

		protected void Context(string message, Action contextBuilder) {
			Debug.Assert (contexts.Count != 0, "'Context' should not be at root");

			var context = new Context(message);
			contexts.Peek ().AddExecutable (context);

			contexts.Push (context);
			contextBuilder ();
			contexts.Pop ();
		}

		protected void It(string message, Action exampleBuilder) {
			var example = new Example (message, exampleBuilder);
			contexts.Peek ().AddExecutable (example);
		}

		protected void BeforeEach(Action beforeEachBuilder) {
			contexts.Peek ().AddBeforeEach (beforeEachBuilder);
		}
	}
}
