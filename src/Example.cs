using System;

namespace UniSpec {
	public class Example : Executable {
		string message;
		Action exampleAction;

		public Example (string message, Action exampleAction) {
			this.message = message;
			this.exampleAction = exampleAction;
		}

		public void Execute (ExecutionContext execContext) {
			execContext.ExecuteBeforeEachActions ();
			exampleAction ();
		}
	}
}