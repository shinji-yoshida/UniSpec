using System.Collections.Generic;
using System;

namespace UniSpec {
	public class Context : Executable {
		string mesage;
		List<Action> beforeEachActions;
		List<Executable> executables;

		public Context (string message) {
			this.mesage = message;
			beforeEachActions = new List<Action> ();
			executables = new List<Executable> ();
		}

		public void AddBeforeEach (Action beforeEachBuilder) {
			beforeEachActions.Add (beforeEachBuilder);
		}

		public void AddExecutable (Executable executable) {
			executables.Add (executable);
		}

		public void Execute (ExecutionContext execContext) {
			execContext.AddBeforeEachActions (beforeEachActions);
			foreach (var each in executables)
				each.Execute (execContext);
			execContext.RemoveBeforeEachActions (beforeEachActions);
		}

		protected void RunBeforeEachs () {
			foreach (var each in beforeEachActions)
				each ();
		}
	}
}