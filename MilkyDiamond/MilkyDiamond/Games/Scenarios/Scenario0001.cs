﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Games.Enemies;
using Charlotte.Common;

namespace Charlotte.Games.Scenarios
{
	class Scenario0001 : IScenario
	{
		private static IEnumerable<bool> GetSeqnencer()
		{
			for (; ; )
			{
				foreach (DDScene scene in DDSceneUtils.Create(10))
				{
					Game.I.AddEnemy(IEnemies.Load(
						new Enemy0001(),
						DDConsts.Screen_W + 50.0,
						100.0 + scene.Rate * 200.0
						));

					for (int c = 0; c < 20; c++)
						yield return true;
				}

				for (int c = 0; c < 60; c++)
					yield return true;

				foreach (DDScene scene in DDSceneUtils.Create(10))
				{
					Game.I.AddEnemy(IEnemies.Load(
						new Enemy0001(),
						DDConsts.Screen_W + 50.0,
						DDConsts.Screen_H - 100.0 - scene.Rate * 200.0
						));

					for (int c = 0; c < 20; c++)
						yield return true;
				}

				for (int c = 0; c < 60; c++)
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
