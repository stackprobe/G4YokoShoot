using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;
using Charlotte.Games.Scenarios;

namespace Charlotte.Worlds
{
	public class World : IDisposable
	{
		public IScenario[] Scenarios;

		// <---- prm

		public static World I = null;

		public World()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			// ルートが分岐したりすんじゃね？

			foreach (IScenario scenario in this.Scenarios)
			{
				using (Game game = new Game())
				{
					game.Scenario = scenario;
					game.Status = new Status();
					game.Perform();
				}
			}
		}
	}
}
