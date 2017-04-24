
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Player2
    {
        Sprite sprite = new Sprite();

        KeyboardState State;

        Game1 Game = null;
        bool IsFalling = true;
        bool IsJumping = false;

        Vector2 Velocity = Vector2.Zero;
        Vector2 position = Vector2.Zero; 

        public Vector2 Position
        {
            get
            {
                return sprite.position;
            }
        }

        public Player2(Game1 Game)
        {
            this.Game = Game;
            IsFalling = true;
            IsJumping = false;
            Velocity = Vector2.Zero;
            position = Vector2.Zero;
        }

        public void Load(ContentManager Content)
        {
            sprite.Load(Content, "hero2");
        }

        public void Update(float DeltaTime)
        {
            UpdateInput(DeltaTime);
            sprite.Update(DeltaTime);
        }

        public void Draw(SpriteBatch Spritebatch)
        {
            sprite.Draw(Spritebatch);
        }

        private void UpdateInput(float DeltaTime)
        {
            bool WasMovingLeft = Velocity.X < 0;
            bool WasMovingRight = Velocity.X > 0;
            bool Falling = IsFalling;

            Vector2 Acceleration = new Vector2(0, Game1.Gravity);

            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                Acceleration.X -= Game1.Acceleration;
            }
            else if (WasMovingLeft == true)
            {
                Acceleration.X += Game1.Friction;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                Acceleration.X += Game1.Acceleration;
            }
            else if (WasMovingRight == true)
            {
                Acceleration.X -= Game1.Friction;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) == true &&
                this.IsJumping == false && Falling == false)
            {
                Acceleration.Y -= Game1.jumpImpulse;
                this.IsJumping = true;
            }

            Velocity += Acceleration * DeltaTime;

            Velocity.X = MathHelper.Clamp(Velocity.X,
                                          -Game1.MaxVelocity.X, Game1.MaxVelocity.X);
            Velocity.Y = MathHelper.Clamp(Velocity.Y,
                                          -Game1.MaxVelocity.Y, Game1.MaxVelocity.Y);

            sprite.position += Velocity * DeltaTime;

            if ((WasMovingLeft && (Velocity.X > 0)) || (WasMovingRight && (Velocity.X < 0)))
            {
                Velocity.X = 0;
            }
            int TX = Game.PixelToTile(sprite.position.X);
            int TY = Game.PixelToTile(sprite.position.Y);

            bool NX = (sprite.position.X) % Game1.Tile != 0;
            bool NY = (sprite.position.Y) % Game1.Tile != 0;
            bool Cell = Game.CellAtTileCoord(TX, TY) != 0;
            bool CellRight = Game.CellAtTileCoord(TX + 1, TY) != 0;
            bool CellDown = Game.CellAtTileCoord(TX, TY + 1) != 0;
            bool CellDiag = Game.CellAtTileCoord(TX + 1, TY + 1) != 0;

            if (this.Velocity.Y > 0)
            {
                if ((CellDown && !Cell) || (CellDiag && !CellRight && NX))
                {
                    sprite.position.Y = Game.TileToPixel(TY);
                    this.Velocity.Y = 0;
                    this.IsFalling = false;
                    this.IsJumping = false;
                    NY = false;
                }
            }
            else if (this.Velocity.Y < 0)
            {
                if ((Cell && !CellDown) || (CellRight && !CellDiag && NX))
                {
                    sprite.position.Y = Game.TileToPixel(TY = 1);
                    this.Velocity.Y = 0;
                    Cell = CellDown;
                    CellRight = CellDiag;
                    NY = false;
                }
            }

            if (this.Velocity.X > 0)
            {
                if ((CellRight && !Cell) || (CellDiag && !CellDown && NY))
                {
                    sprite.position.X = Game.TileToPixel(TX);
                    this.Velocity.X = 0;
                }
            }
            else if (this.Velocity.X < 0)
            {
                if ((Cell && !CellRight) || (CellDown && !CellDiag && NY))
                {
                    sprite.position.X = Game.TileToPixel(TX + 1);
                    this.Velocity.X = 0;
                }
            }
            this.IsFalling = !(CellDown || (NX && CellDiag));
        }
    }
}