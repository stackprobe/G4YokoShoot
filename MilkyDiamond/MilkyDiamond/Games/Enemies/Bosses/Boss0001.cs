using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Game3Common;
using Charlotte.Tools;

namespace Charlotte.Games.Enemies.Bosses
{
	public class Boss0001 : IEnemy
	{
		public double X = DDConsts.Screen_W + 96.0;
		public double Y = DDConsts.Screen_H / 2.0;

		public void Loaded(Tools.D2Point pt)
		{
			// noop
		}

		private IEnumerable<object> GetEachFrameSequencer()
		{
			for (int c = 0; c < 40; c++)
			{
				this.X -= 5.0;

				yield return null;
			}
			for (; ; )
			{
				for (int c = 0; c < 20; c++)
				{
					this.Y += 3.0;

					yield return null;
				}
				for (int c = 0; c < 40; c++)
				{
					this.X -= 3.0;

					yield return null;
				}
				for (int c = 0; c < 40; c++)
				{
					this.Y -= 3.0;

					yield return null;
				}
				for (int c = 0; c < 40; c++)
				{
					this.X += 3.0;

					yield return null;
				}
				for (int c = 0; c < 20; c++)
				{
					this.Y += 3.0;

					yield return null;
				}
			}
		}

		private Func<object> EachFrameSequencer = null;

		public bool EachFrame()
		{
			if (this.EachFrameSequencer == null)
				this.EachFrameSequencer = EnumerableTools.Supplier(GetEachFrameSequencer());

			this.EachFrameSequencer();
			return true;
		}

		public Game3Common.Crash GetCrash()
		{
			return CrashUtils.Circle(new D2Point(this.X, this.Y), 96.0);
		}

		public int HP = 100;

		public bool Crashed(IWeapon weapon)
		{
			this.HP -= weapon.GetAttackPoint();

			if (this.HP <= 0)
			{
				EffectUtils.大爆発(this.X, this.Y);

				return false;
			}
			return true;
		}

		public IEnemies.Kind_e GetKind()
		{
			return IEnemies.Kind_e.ENEMY;
		}

		public void Draw()
		{
			DDDraw.DrawCenter(Ground.I.Picture.Boss0001, this.X, this.Y);
		}
	}
}
