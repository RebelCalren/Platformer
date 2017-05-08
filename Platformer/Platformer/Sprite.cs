using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Platformer
{ 
    class Sprite
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 Offset = Vector2.Zero;

        List<AnimatedTexture> Animations = new List<AnimatedTexture>();
        List<Vector2> AnimationOffSets = new List<Vector2>();

        int CurrentAnimation = 0;

        SpriteEffects Effects = SpriteEffects.None;

        //Texture2D Texture;

        public Sprite()
        {
        }

        public void Add(AnimatedTexture Animation, int XOffSet = 0, int YOffSet = 0)
        {
            Animations.Add(Animation);
            AnimationOffSets.Add(new Vector2(XOffSet, YOffSet));
        }

       /* public void Load(ContentManager Content, string Asset)
        {
            Texture = Content.Load<Texture2D>(Asset);
        }*/

        public void Update(float DeltaTime)
        {
            Animations[CurrentAnimation].UpdateFrame(DeltaTime); 
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            Animations[CurrentAnimation].DrawFrame(SpriteBatch, position + AnimationOffSets[CurrentAnimation], Effects);
        }

        public void SetFlipped(bool State)
        {
            if (State == true)
                Effects = SpriteEffects.FlipHorizontally;
            else
                Effects = SpriteEffects.None;
        }

        public void Pause()
        {
            Animations[CurrentAnimation].Pause();
        }

        public void Play()
        {
            Animations[CurrentAnimation].Play();
        }
    }
}
