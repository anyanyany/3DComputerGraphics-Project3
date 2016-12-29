using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Phong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Camera camera;
        Effect effect;
        Model teapot;
        Model frame;
        Texture2D texture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Vector3 camTarget = new Vector3(0, 0, 0);
            Vector3 camPosition = new Vector3(0, 5, 15);
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.DisplayMode.AspectRatio, 0.0001f, 1000f);
            camera = new Camera(camPosition, camTarget, Vector3.Up, projectionMatrix);
        }

        protected override void LoadContent()
        {
            effect = Content.Load<Effect>("LightWithTexture");
            texture = Content.Load<Texture2D>("rgb");
            teapot = Content.Load<Model>("tea");
            frame = Content.Load<Model>("ad");

            effect.Parameters["ModelTexture"].SetValue(texture);
            effect.Parameters["LightPosition"].SetValue(new Vector3(5, 15, 0));
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            camera.Update();
            base.Update(gameTime);
        }

        protected void DrawScene(Camera _camera)
        {
            GraphicsDevice.Clear(Color.White);
            effect.Parameters["View"].SetValue(_camera.ViewMatrix);
            effect.Parameters["Projection"].SetValue(_camera.ProjectionMatrix);
            effect.Parameters["CameraPosition"].SetValue(_camera.Position);

            Matrix worldMatrix = Matrix.CreateRotationX(-MathHelper.PiOver2) * Matrix.CreateScale(0.2f);
            effect.Parameters["World"].SetValue(worldMatrix);
            foreach (ModelMesh mesh in teapot.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                    part.Effect = effect;
                mesh.Draw();
            }
            worldMatrix = Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(0, 5, -5);
            effect.Parameters["World"].SetValue(worldMatrix);
            foreach (ModelMesh mesh in frame.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                    part.Effect = effect;
                mesh.Draw();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawScene(camera);
            base.Draw(gameTime);
        }
    }
}
