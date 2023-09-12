using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Eksasowt
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private int screenWidth;
        private int screenHeight;
        private List<Texture2D> walkLeftFrames;
        private List<Texture2D> walkRightFrames;
        private List<Texture2D> jumpFrames;
        private Background background;
        private List<Object> platforms;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
            InitializePlatforms();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            List<Texture2D> backgroundTextures = new List<Texture2D>
            {
                Content.Load<Texture2D>("Desert"),
                Content.Load<Texture2D>("Desert2"),
                Content.Load<Texture2D>("Espace1"),
                Content.Load<Texture2D>("Espace2"),
                Content.Load<Texture2D>("Montagne1"),
                Content.Load<Texture2D>("Montagne2"),
                // Ajoutez d'autres textures de fond ici
            };

            background = new Background(backgroundTextures, screenWidth, screenHeight);

            jumpFrames = new List<Texture2D>
            {
                Content.Load<Texture2D>("Saut"),
                Content.Load<Texture2D>("Saut2"),
                Content.Load<Texture2D>("Saut3"),
            };

            walkLeftFrames = new List<Texture2D>
            {
                Content.Load<Texture2D>("gauche1"),
                Content.Load<Texture2D>("gauche2"),
                Content.Load<Texture2D>("gauche3"),
                Content.Load<Texture2D>("gauche4"),
                Content.Load<Texture2D>("gauche5"),
                Content.Load<Texture2D>("gauche6"),
                Content.Load<Texture2D>("gauche7"),
                Content.Load<Texture2D>("gauche8"),
            };

            walkRightFrames = new List<Texture2D>
            {
                Content.Load<Texture2D>("droite1"),
                Content.Load<Texture2D>("droite2"),
                Content.Load<Texture2D>("droite3"),
                Content.Load<Texture2D>("droite4"),
                Content.Load<Texture2D>("droite5"),
                Content.Load<Texture2D>("droite6"),
                Content.Load<Texture2D>("droite7"),
                Content.Load<Texture2D>("droite8"),
            };

            _player = new Player(Content.Load<Texture2D>("static"), jumpFrames, screenWidth, screenHeight, walkLeftFrames, walkRightFrames);
        }

        private void InitializePlatforms()
        {
            platforms = new List<Object>
            {
                new Object(new Vector2(0, screenHeight - 30), Content.Load<Texture2D>("rocher"), "Platform"),
                new Object(new Vector2(200, screenHeight - 100), Content.Load<Texture2D>("rocher"), "Platform"),
                // Ajoutez d'autres plateformes ici
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            background.Update();

            if (_player._position.Y <= 0)
            {
                background.StartTransition();
                InitializePlatforms();
                _player._position.Y = screenHeight - _player._texture.Height;
            }

            _player.Update(gameTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            background.Draw(_spriteBatch);

            foreach (var platform in platforms)
            {
                platform.Draw(_spriteBatch);
            }

            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
