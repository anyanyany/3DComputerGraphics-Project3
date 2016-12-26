using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sphere.Lighting;
using System.Collections.Generic;

namespace Sphere
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Camera camera;
        LightManager lightManager;
        Effect effect;

        Sphere sphere;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Vector3 camTarget = new Vector3(0, 0, 0);
            Vector3 camPosition = new Vector3(2, 1, 2);
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.DisplayMode.AspectRatio, 0.0001f, 100f);
            camera = new Camera(camPosition, camTarget, Vector3.Up, projectionMatrix);
        }

        protected override void LoadContent()
        {
            effect = Content.Load<Effect>("LightWithoutTexture");
            lightManager = new LightManager(effect);

            PointLight pl1 = new PointLight(new Vector3(10, 0, 0), Color.LightYellow, Color.LightYellow, 0.6f, 0.2f, 50.0f, 3.0f);
            lightManager.setPointLight(pl1);
            lightManager.SetEffectParameters();

            LoadModels();
        }

        private void LoadModels()
        {
            sphere = new Sphere(1f, new Vector3(0, 0, 0), Color.Pink, 50, 120);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            camera.Update();
            KeyboardState keyboardNewState = Keyboard.GetState();


            base.Update(gameTime);
        }

        protected void DrawScene(Camera _camera)
        {
            GraphicsDevice.Clear(Color.Black);
            effect.Parameters["View"].SetValue(_camera.ViewMatrix);
            effect.Parameters["Projection"].SetValue(_camera.ProjectionMatrix);
            effect.Parameters["CameraPosition"].SetValue(_camera.Position);
            sphere.Draw(effect, graphics);
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawScene(camera);
            base.Draw(gameTime);
        }
    }
}
