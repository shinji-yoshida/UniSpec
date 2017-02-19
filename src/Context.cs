using System.Collections.Generic;
using System;

namespace UniSpec {
	public class Context : Executable {
		string message;
		List<Action> beforeEachActions;
		List<Executable> executables;

		public Context (string message) {
			this.message = message;
			beforeEachActions = new List<Action> ();
			executables = new List<Executable> ();
		}

		public void AddBeforeEach (Action beforeEachBuilder) {
			beforeEachActions.Add (beforeEachBuilder);
		}

		public void AddExecutable (Executable executable) {
			executables.Add (executable);
		}

		public SpecResult Execute (ExecutionContext execContext) {
			var specResult = new CompositeSpecResult (message);
			execContext.AddBeforeEachActions (beforeEachActions);
			foreach (var each in executables)
				specResult.AddSpecResult(each.Execute (execContext));
			
			execContext.RemoveBeforeEachActions (beforeEachActions);
			return specResult;
		}

		protected void RunBeforeEachs () {
			foreach (var each in beforeEachActions)
				each ();
		}
	}
}