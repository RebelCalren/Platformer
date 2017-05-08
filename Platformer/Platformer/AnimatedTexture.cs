#region File Description
//-----------------------------------------------------------------------------
// AnimatedTexture.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class AnimatedTexture
    {
        private int framecount;
        private Texture2D myTexture;
        private float TimePerFrame;
        private int Frame;
        private float TotalElapsed;
        private bool Paused;

        public float Rotation, Scale, Depth;
        public Vector2 Origin;
        public AnimatedTexture(Vector2 origin, float rotation, 
            float scale, float depth)
        {
            this.Origin = origin;
            this.Rotation = rotation;
            this.Scale = scale;
            this.Depth = depth;
        }
        public void Load(ContentManager content, string asset, 
            int frameCount, int framesPerSec)
        {
            framecount = frameCount;
            myTexture = content.Load<Texture2D>(asset);
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {
            if (Paused)
                return;
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % framecount;
                TotalElapsed -= TimePerFrame;
            }
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch Batch, Vector2 ScreenPos, SpriteEffects Effects = SpriteEffects.None)
        {
            DrawFrame(Batch, Frame, ScreenPos, Effects);
        }
        public void DrawFrame(SpriteBatch Batch, int Frame, Vector2 ScreenPos, SpriteEffects Effects = SpriteEffects.None)
        {
            int FrameWidth = myTexture.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * Frame, 0,
                FrameWidth, myTexture.Height);
            Batch.Draw(myTexture, ScreenPos, sourcerect, Color.White,
                Rotation, Origin, Scale, Effects, Depth);
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }

    }
}
