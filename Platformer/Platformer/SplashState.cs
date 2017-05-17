using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    public class SplashState : AIE.State
    {
        SpriteFont Font = null;
        float Timer = 3;

        public SplashState() : base()
        {
        }

        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if (Font == null)
            {
                Font = Content.Load<SpriteFont>("Arial");
            }

            Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer <= 0)
            {
                AIE.StateManager.ChangeState("GAME");
                Timer = 3;
            }
        }

        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();
            SB.DrawString(Font, "Splash", new Vector2(200, 200), Color.White);
            SB.End();
        }

        public override void CleanUp()
        {
            Font = null;
            Timer = 3;
        }
    }
}
