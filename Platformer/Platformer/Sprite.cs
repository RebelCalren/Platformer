using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{ 
    class Sprite
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 Offset = Vector2.Zero;

        Texture2D Texture;

        public Sprite()
        {
        }

        public void Load(ContentManager Content, string Asset)
        {
            Texture = Content.Load<Texture2D>(Asset);
        }

        public void Update(float DeltaTime)
        {

        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, position + Offset, Color.White);
        }
    }
}
