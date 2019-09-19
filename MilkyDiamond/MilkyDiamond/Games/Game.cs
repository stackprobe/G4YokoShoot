﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Game3Common;
using Charlotte.Games.Enemies;
using Charlotte.Games.Walls;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public IScenario Scenario;
		public Status Status;

		// <---- prm

		public static Game I = null;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			this.WallScreen.Dispose();
			this.WallScreen = null;

			I = null;
		}

		private Player Player = new Player();

		public int Frame = 0;

		public void Perform()
		{
			this.Player.X = DDConsts.Screen_W / 4;
			this.Player.Y = DDConsts.Screen_H / 2;

			this.Player.BornScene.Fire();

			DDCurtain.SetCurtain(10);

			for (; ; this.Frame++)
			{
				// プレイヤー行動
				{
					bool bornOrDead = this.Player.BornScene.IsFlaming() || this.Player.DeadScene.IsFlaming();
					bool dead = this.Player.DeadScene.IsFlaming();
					double xa = 0.0;
					double ya = 0.0;

					if (!dead && 1 <= DDInput.DIR_4.GetInput()) // 左移動
					{
						xa = -1.0;
					}
					if (!dead && 1 <= DDInput.DIR_6.GetInput()) // 右移動
					{
						xa = 1.0;
					}
					if (!dead && 1 <= DDInput.DIR_8.GetInput()) // 上移動
					{
						ya = -1.0;
					}
					if (!dead && 1 <= DDInput.DIR_2.GetInput()) // 下移動
					{
						ya = 1.0;
					}
					double speed = 6.0;

					if (!dead && 1 <= DDInput.A.GetInput()) // 低速ボタン押下中
					{
						speed = 3.0;
					}
					this.Player.X += xa * speed;
					this.Player.Y += ya * speed;

					DDUtils.Range(ref this.Player.X, 0.0, DDConsts.Screen_W);
					DDUtils.Range(ref this.Player.Y, 0.0, DDConsts.Screen_H);

					if (!bornOrDead && 1 <= DDInput.B.GetInput()) // 攻撃ボタン押下中
					{
						this.Player.Shoot();
					}
				}

				{
					DDScene scene = this.Player.BornScene.GetScene();

					if (scene != null)
					{
						if (scene.Remaining == 0)
						{
							this.Player.MutekiScene.FireDelay();
						}
					}
				}

				{
					DDScene scene = this.Player.DeadScene.GetScene();

					if (scene != null)
					{
						if (scene.Remaining == 0)
						{
							if (this.Status.RemainingLiveCount <= 0)
								break;

							this.Status.RemainingLiveCount--;
							this.Player.BornScene.FireDelay();
						}
					}
				}

				{
					DDScene scene = this.Player.MutekiScene.GetScene();

					if (scene != null)
					{
						// noop
					}
				}

				this.Scenario.EachFrame();

				this.EnemyEachFrame();
				this.WeaponEachFrame();

				// Crash
				{
					Crash playerCrash = CrashUtils.Point(new D2Point(this.Player.X, this.Player.Y));

					foreach (WeaponBox weapon in this.Weapons.Iterate())
						weapon.Crash = weapon.Value.GetCrash();

					foreach (EnemyBox enemy in this.Enemies.Iterate())
					{
						Crash enemyCrash = enemy.Value.GetCrash();

						foreach (WeaponBox weapon in this.Weapons.Iterate())
						{
							if (enemyCrash.IsCrashed(weapon.Crash))
							{
								if (enemy.Value.Crashed(weapon.Value) == false) // ? 消滅
									enemy.Dead = true;

								if (weapon.Value.Crashed(enemy.Value) == false) // ? 消滅
									weapon.Dead = true;
							}
						}
						this.Weapons.RemoveAll(weapon => weapon.Dead);

						if (this.Player.BornScene.IsFlaming() == false &&
							this.Player.DeadScene.IsFlaming() == false &&
							this.Player.MutekiScene.IsFlaming() == false && enemyCrash.IsCrashed(playerCrash))
						{
							this.Player.DeadScene.Fire();
						}
					}
					this.Enemies.RemoveAll(enemy => enemy.Dead);
				}

				// ここから描画

				this.DrawWall();
				this.Player.Draw();
				this.DrawEnemies();
				this.DrawWeapons();

				DDPrint.SetPrint();
				DDPrint.Print(DDEngine.FrameProcessingMillis_Worst + " " + this.Enemies.Count);

				DDEngine.EachFrame();
			}
		}

		private IWall LastWall = null;
		private IWall Wall = new WallDark();
		private int WallChangeNumer = -1;
		private int WallChangeDenom = -1;
		private DDSubScreen WallScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);

		public void SetWall(IWall wall, int denom = 180)
		{
			this.LastWall = this.Wall;
			this.Wall = wall;
			this.WallChangeNumer = 0;
			this.WallChangeDenom = denom;
		}

		private void DrawWall()
		{
			if (this.LastWall == null)
			{
				this.Wall.Draw();
			}
			else
			{
				double a = this.WallChangeNumer * 1.0 / this.WallChangeDenom;

				this.LastWall.Draw();

				using (this.WallScreen.Section())
				{
					this.Wall.Draw();
				}

				DDDraw.SetAlpha(a);
				DDDraw.DrawSimple(this.WallScreen.ToPicture(), 0, 0);
				DDDraw.Reset();

				this.WallChangeNumer++;

				if (this.WallChangeDenom <= this.WallChangeNumer)
				{
					this.LastWall = null;
					this.WallChangeNumer = -1;
					this.WallChangeDenom = -1;
				}
			}
		}

		private class EnemyBox
		{
			public IEnemy Value;
			public bool Dead;
		}

		private DDList<EnemyBox> Enemies = new DDList<EnemyBox>();

		public void AddEnemy(IEnemy enemy)
		{
			this.Enemies.Add(new EnemyBox()
			{
				Value = enemy,
			});
		}

		private void EnemyEachFrame()
		{
			foreach (EnemyBox enemy in this.Enemies.Iterate())
			{
				if (enemy.Value.EachFrame() == false) // ? 消滅
				{
					enemy.Dead = true;
				}
			}
			this.Enemies.RemoveAll(enemy => enemy.Dead);
		}

		private void DrawEnemies()
		{
			foreach (EnemyBox enemy in this.Enemies.Iterate())
			{
				enemy.Value.Draw();
			}
		}

		private class WeaponBox
		{
			public IWeapon Value;
			public Crash Crash;
			public bool Dead;
		}

		private DDList<WeaponBox> Weapons = new DDList<WeaponBox>();

		public void AddWeapon(IWeapon weapon)
		{
			this.Weapons.Add(new WeaponBox()
			{
				Value = weapon,
			});
		}

		private void WeaponEachFrame()
		{
			foreach (WeaponBox weapon in this.Weapons.Iterate())
			{
				if (weapon.Value.EachFrame() == false) // ? 消滅
				{
					weapon.Dead = true;
				}
			}
			this.Weapons.RemoveAll(weapon => weapon.Dead);
		}

		private void DrawWeapons()
		{
			foreach (WeaponBox weapon in this.Weapons.Iterate())
			{
				weapon.Value.Draw();
			}
		}
	}
}
