using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        Player player = null;
        Player2 player2 = null;

        Camera2D Camera = null;
        TiledMap Map = null;
        TiledTileLayer CollisionLayer;

        public static int Tile = 64;
        // abitary choice for 1M (1 Tile = 1 Meter)
        public static float Meter = Tile;
        // very exaggerated gravity (6x)
        public static float Gravity = Meter * 9.8f * 6.0f;
        // max vertical speed (10 tiles/sec horizontal, 15 tiles/sec vertical)
        public static Vector2 MaxVelocity = new Vector2(Meter * 10, Meter * 15);
        // horizontal Acceleration - 1/2 Second to reach max velocity
        public static float Acceleration = MaxVelocity.X * 2;
        // horizontal friction     - take 1/6 second to stop from max Velocity
        public static float Friction = MaxVelocity.X * 6;
        // (a large) instantaneous jump impulse
        public static float jumpImpulse = Meter * 1500;

        public int ScreenWidth
        {
            get
            {
                return Graphics.GraphicsDevice.Viewport.Width;
            }
                
        }

        public int ScreenHeight
        {
            get
            {
                return Graphics.GraphicsDevice.Viewport.Height;
            }
        }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(this);
            player2 = new Player2(this); 
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.Load(Content);
            player2.Load(Content);

            var ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenWidth, ScreenHeight);

            Camera = new Camera2D(ViewportAdapter);
            Camera.Position = new Vector2(0, ScreenHeight);

            Map = Content.Load<TiledMap>("Level1");
            foreach (TiledTileLayer Layer in Map.TileLayers)
            {
                if (Layer.Name == "Collisions")
                    CollisionLayer = Layer;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()// when i double click Content.mcgb thisd happens

        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Update(DeltaTime);
            player2.Update(DeltaTime);
            Camera.Position = player.Position - new Vector2(ScreenWidth / 2, ScreenHeight / 2); 

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = Camera.GetViewMatrix();

            SpriteBatch.Begin(transformMatrix: transformMatrix);

            player.Draw(SpriteBatch);
            player2.Draw(SpriteBatch);
            Map.Draw(SpriteBatch);
            SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public int PixelToTile(float PixelCoord)
        {
            return (int)Math.Floor(PixelCoord / Tile);
        }

        public int TileToPixel(int TileCoord)
        {
            return Tile * TileCoord;
        }

        public int CellAtPixelCoord(Vector2 PixelCoords)
        {
            if (PixelCoords.X < 0 ||
                PixelCoords.X > Map.WidthInPixels || PixelCoords.Y < 0)
                return 1;
            if (PixelCoords.Y > Map.HeightInPixels)
                return 0;
            return CellAtTileCoord
                (PixelToTile(PixelCoords.X), PixelToTile(PixelCoords.Y));
        }

        public int CellAtTileCoord(int TX, int TY)
        {
            if (TX < 0 || TX >= Map.Width || TY < 0)
                return 1;
            if (TY >= Map.Height)
                return 0;

            TiledTile Tile = CollisionLayer.GetTile(TX, TY);
            return Tile.Id;
        }
    }
}
