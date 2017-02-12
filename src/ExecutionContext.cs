using System.Collections.Generic;
using System;

namespace UniSpec {
	public class ExecutionContext {
		List<Action> beforeEachActions = new List<Action>();

		public void AddBeforeEachActions (List<Action> actions) {
			beforeEachActions.AddRange (actions);
		}

		public void RemoveBeforeEachActions (List<Action> actions) {
			foreach (var each in actions)
				beforeEachActions.Remove (each);
		}

		public void ExecuteBeforeEachActions () {
			foreach (var each in beforeEachActions)
				each ();
		}
	}
}
