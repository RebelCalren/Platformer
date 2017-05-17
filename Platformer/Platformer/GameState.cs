using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class GameState : AIE.State
    {
        bool IsLoaded = false;
        SpriteFont Font = null;

        public GameState() : base()
        {
        }

        public override void Update(ContentManager Content, GameTime GT)
        {
            if (IsLoaded == false)
            {
                IsLoaded = true;
                Font = Content.Load<SpriteFont>("Arial");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                AIE.StateManager.ChangeState("GAMEOVER");
            }
        }

        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();
            SB.DrawString(Font, "GameState", new Vector2(200, 200), Color.White);
            SB.End();
        }

        public override void CleanUp()
        {
            Font = null;
            IsLoaded = false;
        }
    }
}
