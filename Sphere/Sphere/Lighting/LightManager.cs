using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphere.Lighting
{
    class LightManager
    {
        public List<Effect> effects;
        List<PointLight> pointLights;

        public LightManager(List<Effect> _effects)
        {
            effects = _effects;
            pointLights = new List<PointLight>();
        }

        public void addPointLight(PointLight _light)
        {
            pointLights.Add(_light);
        }

        public void SetEffectParameters()
        {
            int allLights = pointLights.Count;
            Vector3[] Position = new Vector3[allLights];
            float[] Id = new float[allLights];
            float[] Is = new float[allLights];
            Vector3[] Kd = new Vector3[allLights];
            Vector3[] Ks = new Vector3[allLights];
            float[] Attenuation = new float[allLights];
            float[] Falloff = new float[allLights];

            for (int i = 0; i < pointLights.Count; i++)
            {
                Position[i] = pointLights.ElementAt(i).Position;
                Id[i] = pointLights.ElementAt(i).Id;
                Is[i] = pointLights.ElementAt(i).Is;
                Kd[i] = pointLights.ElementAt(i).Kd.ToVector3();
                Ks[i] = pointLights.ElementAt(i).Ks.ToVector3();
                Attenuation[i] = pointLights.ElementAt(i).Attenuation;
                Falloff[i] = pointLights.ElementAt(i).Falloff;
            }


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

            foreach (Effect effect in effects)
            {
                effect.Parameters["LightPosition"].SetValue(Position);
                effect.Parameters["Id"].SetValue(Id);
                effect.Parameters["Is"].SetValue(Is);
                effect.Parameters["Kd"].SetValue(Kd);
                effect.Parameters["Ks"].SetValue(Ks);
                effect.Parameters["Attenuation"].SetValue(Attenuation);
                effect.Parameters["Falloff"].SetValue(Falloff);
                effect.Parameters["pentagonPoints"].SetValue(pentagonPoints);
                effect.Parameters["hexagonPoints"].SetValue(hexagonPoints);
            }

        }
    }
}
