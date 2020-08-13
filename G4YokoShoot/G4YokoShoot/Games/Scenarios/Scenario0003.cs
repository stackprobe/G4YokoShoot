using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Walls;
using Charlotte.Games.Enemies;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Games.Enemies.Bosses;

namespace Charlotte.Games.Scenarios
{
	public class Scenario0003 : IScenario
	{
		private static IEnumerable<bool> GetSeqnencer()
		{
			for (; ; )
			{
				Game.I.SetWall(new Wall0003());

				Game.I.AddEnemy(IEnemies.Load(new Enemy0002(), DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

				for (int c = 0; c < 500; c++)
					yield return true;

				Game.I.SetWall(new Wall0004());

				Game.I.AddEnemy(IEnemies.Load(new Enemy0001(), DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

				for (int c = 0; c < 500; c++)
					yield return true;

				Game.I.AddEnemy(IEnemies.Load(new Boss0001(), -1.0, 0.0));

				for (; ; )
					yield return true; // HACK
			}
		}

		private Func<bool> Sequencer = EnumerableTools.Supplier(GetSeqnencer());

		public bool EachFrame()
		{
			return this.Sequencer();
		}
	}
}
