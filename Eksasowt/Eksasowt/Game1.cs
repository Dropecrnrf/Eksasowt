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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            _player = new Player(Content.Load<Texture2D>("static"), screenWidth, screenHeight, walkLeftFrames, walkRightFrames);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Dessin du joueur
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
