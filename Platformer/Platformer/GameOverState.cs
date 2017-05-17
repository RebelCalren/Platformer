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
    class GameOverState : AIE.State
    {
        bool IsLoaded = false;
        SpriteFont Font = null;

        KeyboardState OldState;

        public GameOverState() : base()
        {
        }

        public override void Update(ContentManager Content, GameTime GT)
        {
            if (IsLoaded == false)
            {
                IsLoaded = true;
                Font = Content.Load<SpriteFont>("Arial");
                OldState = Keyboard.GetState();
            }

            KeyboardState NewState = Keyboard.GetState();

            if (NewState.IsKeyDown(Keys.Enter) == true)
            {
                if (OldState.IsKeyDown(Keys.Enter) == true)
                {
                    AIE.StateManager.ChangeState("Splash");
                }
            }
            OldState = NewState; 
        }

        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();
            SB.DrawString(Font, "GAME OVER....", new Vector2(200, 200), Color.White);
            SB.End();
        }

        public override void CleanUp()
        {
            Font = null;
            IsLoaded = false;
        }
    }
}
