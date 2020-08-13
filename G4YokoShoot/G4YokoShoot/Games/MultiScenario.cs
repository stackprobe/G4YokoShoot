using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class MultiScenario : IScenario
	{
		public MultiScenario(params IScenario[] scenarios)
			: this((IEnumerable<IScenario>)scenarios)
		{ }

		public MultiScenario(IEnumerable<IScenario> scenarios)
		{
			this.Sequencer = EnumerableTools.Supplier(this.GetSeqnencer(scenarios));
		}

		private IEnumerable<bool> GetSeqnencer(IEnumerable<IScenario> scenarios)
		{
			foreach (IScenario scenario in scenarios)
				while (scenario.EachFrame())
					yield return true;
		}

		private Func<bool> Sequencer;

		public bool EachFrame()
		{
			return this.Sequencer();
		}
	}
}
