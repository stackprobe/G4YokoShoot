using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Game3Common;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Games.Enemies
{
	public class Enemy0001 : IEnemy
	{
		public double X;
		public double Y;

		public void Loaded(Tools.D2Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		public bool EachFrame()
		{
			this.X -= 3.0;

			return DDUtils.IsOutOfScreen(new D2Point(this.X, this.Y), 48.0) == false;
		}

		public Game3Common.Crash GetCrash()
		{
			return CrashUtils.Circle(new D2Point(this.X, this.Y), 48.0);
		}

		public bool Crashed(IWeapon weapon)
		{
			EffectUtils.中爆発(this.X, this.Y);

			return false;
		}

		public IEnemies.Kind_e GetKind()
		{
			return IEnemies.Kind_e.ENEMY;
		}

		public void Draw()
		{
			DDDraw.DrawCenter(Ground.I.Picture.Enemy0001, this.X, this.Y);
		}
	}
}
