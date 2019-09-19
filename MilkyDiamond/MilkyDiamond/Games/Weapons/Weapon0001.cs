using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Game3Common;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Games.Weapons
{
	public class Weapon0001 : IWeapon
	{
		private double X;
		private double Y;

		public void Loaded(Tools.D2Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		public bool EachFrame()
		{
			this.X += 10.0;

			return DDUtils.IsOutOfScreen(new D2Point(this.X, this.Y), 16.0) == false;
		}

		public Game3Common.Crash GetCrash()
		{
			return CrashUtils.Circle(new D2Point(this.X, this.Y), 16.0);
		}

		public bool Crashed(IEnemy enemy)
		{
			if (enemy.GetKind() != IEnemies.Kind_e.SHOT)
			{
				EffectUtils.小爆発(this.X, this.Y);

				return false;
			}
			return true;
		}

		public int GetAttackPoint()
		{
			return 1;
		}

		public void Draw()
		{
			DDDraw.DrawCenter(Ground.I.Picture.Weapon0001, this.X, this.Y);
		}
	}
}
