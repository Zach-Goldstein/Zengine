using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Reflection;
using System.Runtime;

namespace Engine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Core : Game
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public static Core Instance { get; private set; }
        public static string ContentDirectory { get => Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Instance.Content.RootDirectory); }

        // Some screen stuff
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static string Title;
        public static Matrix Camera;

        // Scene stuff
        private Scene scene;

        private Scene nextScene;

        public Scene Scene
        {
            get => scene;
            set => nextScene = value;
        }

        public Core(string title, int width, int height)
        {
            Instance = this;
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Width = width;
            Height = height;
            Title = title;

            Graphics.PreferredBackBufferWidth = Width;
            Graphics.PreferredBackBufferHeight = Height;

            Camera = MatrixTransforms.ScaleMatrix(1);

            IsMouseVisible = true;
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Input.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.Update();

            if (scene != null)
                scene.Update();

            if (scene != nextScene)
                scene = nextScene;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera);
            if (scene != null)
                scene.Draw();
            SpriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
