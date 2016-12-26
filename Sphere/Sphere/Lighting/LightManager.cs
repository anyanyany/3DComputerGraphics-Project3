using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sphere.Lighting
{
    class LightManager
    {
        public Effect effect;
        PointLight pointLight;

        public LightManager(Effect _effect)
        {
            effect = _effect;
            pointLight = null;
        }

        public void setPointLight(PointLight _light)
        {
            pointLight = _light;
        }

        public void SetEffectParameters()
        {
            if (pointLight == null)
                return;

            Vector3[] pentagonPoints = new Vector3[12];
            Vector3[] hexagonPoints = new Vector3[20];

            pentagonPoints[0] = new Vector3(0.0000f, 0.6045f, 0.9780f);
            pentagonPoints[1] = new Vector3(-0.0000f, -0.6045f, 0.9780f);
            pentagonPoints[2] = new Vector3(0.9780f, -0.0000f, 0.6045f);
            pentagonPoints[3] = new Vector3(0.6045f, -0.9780f, 0.0000f);
            pentagonPoints[4] = new Vector3(0.9780f, -0.0000f, -0.6045f);
            pentagonPoints[5] = new Vector3(-0.0000f, -0.6045f, -0.9780f);
            pentagonPoints[6] = new Vector3(0.0000f, 0.6045f, -0.9780f);
            pentagonPoints[7] = new Vector3(-0.9780f, 0.0000f, -0.6045f);
            pentagonPoints[8] = new Vector3(-0.6045f, 0.9780f, -0.0000f);
            pentagonPoints[9] = new Vector3(-0.9780f, 0.0000f, 0.6045f);
            pentagonPoints[10] = new Vector3(-0.6045f, -0.9780f, -0.0000f);
            pentagonPoints[11] = new Vector3(0.6045f, 0.9780f, 0.0000f);

            hexagonPoints[0] = new Vector3(0.3568f, -0.0000f, 0.9342f);
            hexagonPoints[1] = new Vector3(0.5774f, 0.5774f, 0.5774f);
            hexagonPoints[2] = new Vector3(0.0000f, 0.9342f, 0.3568f);
            hexagonPoints[3] = new Vector3(-0.5774f, 0.5774f, 0.5774f);
            hexagonPoints[4] = new Vector3(-0.3568f, 0.0000f, 0.9342f);
            hexagonPoints[5] = new Vector3(-0.5774f, -0.5774f, 0.5774f);
            hexagonPoints[6] = new Vector3(-0.0000f, -0.9342f, 0.3568f);
            hexagonPoints[7] = new Vector3(0.5774f, -0.5774f, 0.5774f);
            hexagonPoints[8] = new Vector3(0.9342f, 0.3568f, 0.0000f);
            hexagonPoints[9] = new Vector3(0.9342f, -0.3568f, 0.0000f);
            hexagonPoints[10] = new Vector3(-0.0000f, -0.9342f, -0.3568f);
            hexagonPoints[11] = new Vector3(0.5774f, -0.5774f, -0.5774f);
            hexagonPoints[12] = new Vector3(0.5774f, 0.5774f, -0.5774f);
            hexagonPoints[13] = new Vector3(0.3568f, -0.0000f, -0.9342f);
            hexagonPoints[14] = new Vector3(-0.5774f, -0.5774f, -0.5774f);
            hexagonPoints[15] = new Vector3(-0.3568f, 0.0000f, -0.9342f);
            hexagonPoints[16] = new Vector3(0.0000f, 0.9342f, -0.3568f);
            hexagonPoints[17] = new Vector3(-0.5774f, 0.5774f, -0.5774f);
            hexagonPoints[18] = new Vector3(-0.9342f, -0.3568f, -0.0000f);
            hexagonPoints[19] = new Vector3(-0.9342f, 0.3568f, -0.0000f);

            effect.Parameters["LightPosition"].SetValue(pointLight.Position);
            effect.Parameters["Id"].SetValue(pointLight.Id);
            effect.Parameters["Is"].SetValue(pointLight.Is);
            effect.Parameters["Kd"].SetValue(pointLight.Kd.ToVector3());
            effect.Parameters["Ks"].SetValue(pointLight.Ks.ToVector3());
            effect.Parameters["Attenuation"].SetValue(pointLight.Attenuation);
            effect.Parameters["Falloff"].SetValue(pointLight.Falloff);
            effect.Parameters["pentagonPoints"].SetValue(pentagonPoints);
            effect.Parameters["hexagonPoints"].SetValue(hexagonPoints);
        }
    }
}
