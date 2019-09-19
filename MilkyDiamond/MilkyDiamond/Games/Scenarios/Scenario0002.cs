using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Walls;
using Charlotte.Tools;

namespace Charlotte.Games.Scenarios
{
	public class Scenario0002 : IScenario
	{
		private static IEnumerable<bool> GetSeqnencer()
		{
			for (; ; )
			{
				Game.I.SetWall(new Wall0003());

				for (int c = 0; c < 500; c++)
					yield return true;

				Game.I.SetWall(new Wall0004());

				for (int c = 0; c < 500; c++)
					yield return true;
			}
		}

		private Func<bool> Sequencer = EnumerableTools.Supplier(GetSeqnencer());

		public bool EachFrame()
		{
			return this.Sequencer();
		}
	}
}
