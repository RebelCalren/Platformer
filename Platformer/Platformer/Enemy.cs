using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Enemy
    {
        Sprite Sprite = new Sprite();
        Game1 Game = null;
        Vector2 Velocity = Vector2.Zero;

        float Pause = 0;
        bool MoveRight = true;

        static float EnemyAcceleration = Game1.Acceleration / 5.0f;
        static Vector2 EnemyMaxVelocity = Game1.MaxVelocity / 5.0f;

        public Vector2 Position
        {
            get
            {
                return Sprite.position;
            }
            set
            {
                Sprite.position = value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return Sprite.Bounds;
            }
        }

        public Enemy(Game1 Game)
        {
            this.Game = Game;
            Velocity = Vector2.Zero;
        }

        public void Load(ContentManager Content)
        {
            AnimatedTexture Animation = new AnimatedTexture(Vector2.Zero, 0, 1, 1);
            Animation.Load(Content, "Enemy", 4, 5);

            Sprite.Add(Animation, 16, 0);
        }

        public void Update (float DeltaTime)
        {
            Sprite.Update(DeltaTime);

            if (Pause > 0)
            {
                Pause -= DeltaTime;
            }
            else
            {
                float DDX = 0;

                int TX = Game.PixelToTile(Position.X);
                int TY = Game.PixelToTile(Position.Y);
                bool NX = (Position.X) % Game1.Tile != 0;
                bool NY = (Position.Y) % Game1.Tile != 0;

                bool Cell = Game.CellAtTileCoord(TX, TY) != 0;
                bool CellRight = Game.CellAtTileCoord(TX + 1, TY) != 0;
                bool CellDown = Game.CellAtTileCoord(TX, TY + 1) != 0;
                bool CellDiag = Game.CellAtTileCoord(TX + 1, TY + 1) != 0;

                if (MoveRight)
                {
                    if (CellDiag && !CellRight)
                    {
                        DDX = DDX = EnemyAcceleration;
                    }
                    else
                    {
                        this.Velocity.X = 0;
                        this.MoveRight = false;
                        this.Pause = 0.5f;
                    }
                }

                if (!this.MoveRight)
                {
                    if (CellDown && !Cell)
                    {
                        DDX = DDX - EnemyAcceleration;
                    }

                    else
                    {
                        this.Velocity.X = 0;
                        this.MoveRight = true;
                        this.Pause = 0.5f;
                    }
                }

                Position = new Vector2((float)Math.Floor(Position.X + (DeltaTime * Velocity.X)), Position.Y);
                Velocity.X = MathHelper.Clamp(Velocity.X + (DeltaTime * DDX), -EnemyMaxVelocity.X, EnemyMaxVelocity.X);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        

    }
}
