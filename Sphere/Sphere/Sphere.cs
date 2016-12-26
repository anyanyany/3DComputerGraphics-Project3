using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sphere
{
    public class Sphere
    {
        public VertexPositionNormalTexture[] vertices;
        Matrix worldMatrix;
        Vector3 center;
        Color color;
        float shininess;
        float radius;
        int precision;

        public Sphere(float _radius, Vector3 _center, Color _color, float _shininess, int _precision)
        {
            radius = _radius;
            color = _color;
            precision = _precision;
            center = _center;
            shininess = _shininess;

            int n = precision * precision * 6;
            vertices = new VertexPositionNormalTexture[n];
            worldMatrix = Matrix.CreateTranslation(center);


            Vector3[] points = new Vector3[precision * precision];

            Vector3 rad = new Vector3((float)Math.Abs(radius), 0, 0);
            for (int x = 0; x < precision; x++) //100 circles, difference between each is 3.6 degrees
            {
                float difx = 360.0f / precision;
                for (int y = 0; y < precision; y++) //100 veritces, difference between each is 3.6 degrees 
                {
                    float dify = 360.0f / precision;
                    Matrix zrot = Matrix.CreateRotationZ(MathHelper.ToRadians(y * dify)); //rotate vertex around z
                    Matrix yrot = Matrix.CreateRotationY(MathHelper.ToRadians(x * difx)); //rotate circle around y
                    Vector3 point = Vector3.Transform(Vector3.Transform(rad, zrot), yrot);//transformation

                    points[x + y * precision] = point;
                }
            }


            int i = 0;
            for (int x = 0; x < precision; x++)
            {
                for (int y = 0; y < precision; y++)
                {
                    int s1 = x == (precision - 1) ? 0 : x + 1;
                    int s2 = y == (precision - 1) ? 0 : y + 1;
                    short upperLeft = (short)(x * precision + y);
                    short upperRight = (short)(s1 * precision + y);
                    short lowerLeft = (short)(x * precision + s2);
                    short lowerRight = (short)(s1 * precision + s2);

                    Vector3 normal = points[upperLeft] - center;
                    normal.Normalize();
                    vertices[i++] = new VertexPositionNormalTexture(points[upperLeft], normal, Vector2.Zero);

                    normal = points[upperRight] - center;
                    normal.Normalize();
                    vertices[i++] = new VertexPositionNormalTexture(points[upperRight], normal, Vector2.Zero);

                    normal = points[lowerLeft] - center;
                    normal.Normalize();
                    vertices[i++] = new VertexPositionNormalTexture(points[lowerLeft], normal, Vector2.Zero);

                    normal = points[lowerLeft] - center;
                    normal.Normalize();
                    vertices[i++] = new VertexPositionNormalTexture(points[lowerLeft], normal, Vector2.Zero);

                    normal = points[upperRight] - center;
                    normal.Normalize();
                    vertices[i++] = new VertexPositionNormalTexture(points[upperRight], normal, Vector2.Zero);

                    normal = points[lowerRight] - center;
                    normal.Normalize();
                    vertices[i++] = new VertexPositionNormalTexture(points[lowerRight], normal, Vector2.Zero);
                }
            }
        }


        public void Draw(Effect effect, GraphicsDeviceManager graphics)
        {
            effect.Parameters["World"].SetValue(worldMatrix);
            effect.Parameters["Ka"].SetValue(color.ToVector3());
            effect.Parameters["Shininess"].SetValue(shininess);

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);
            }
        }
    }
}
