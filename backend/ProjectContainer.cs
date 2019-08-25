using System.Collections.Generic;
using SMWControlibBackend.Interaction;
using SMWControlibBackend.Graphics.Frames;
using System.Xml.Serialization;
using System.IO;
using SMWControlibBackend.Graphics;
using System.Text.RegularExpressions;

namespace SMWControlibBackend
{
    public class ProjectContainer
    {
        public byte[] SP12;
        public byte[] SP34;
        [XmlArray("HitboxContainers")]
        [XmlArrayItem("HitboxContainer", typeof(HitboxContainer))]
        [XmlArrayItem("RectangleHitboxContainer", typeof(RectangleHitboxContainer))]
        [XmlArrayItem("InteractionPointContainer", typeof(InteractionPointContainer))]
        public HitboxContainer[] Hitboxes;
        public InteractionPointContainer[] InteractionPoints;
        public FrameContainer[] Frames;
        public AnimationContainer[] Animations;
        public string Code;
        public GlobalColorPaletteContainer GlobalPalette;

        public void GetAttributes(Frame[] frames, Animation[] animations, string code, byte[] sp12, byte[] sp34)
        {
            GlobalPalette = new GlobalColorPaletteContainer();
            GlobalPalette.ToGlobalColorPaletteContainer();
            Code = code;
            SP12 = sp12;
            SP34 = sp34;
            List<HitBox> hbs = new List<HitBox>();
            List<InteractionPoint> ips = new List<InteractionPoint>();
            foreach(Frame f in frames)
            {
                foreach (HitBox hb in f.HitBoxes)
                {
                    if(!hbs.Contains(hb))
                    {
                        hbs.Add(hb);
                    }
                }

                foreach (InteractionPoint hb in f.InteractionPoints)
                {
                    if (!ips.Contains(hb))
                    {
                        ips.Add(hb);
                    }
                }
            }

            Hitboxes = new HitboxContainer[hbs.Count];
            int i = 0;

            foreach (HitBox hb in hbs)
            {
                if(hb.GetType() == typeof(RectangleHitBox))
                {
                    Hitboxes[i] = new RectangleHitboxContainer();
                }
                else
                {
                    Hitboxes[i] = new HitboxContainer();
                }
                Hitboxes[i].ToHitboxContainer(hb);
                Hitboxes[i].Name = "" + i + "_" + Hitboxes[i].Name;
                i++;
            }

            InteractionPoints = new InteractionPointContainer[ips.Count];
            i = 0;

            foreach (InteractionPoint hb in ips)
            {
                InteractionPoints[i] = new InteractionPointContainer();
                InteractionPoints[i].ToHitboxContainer(hb);
                InteractionPoints[i].Name = "" + i + "_" + InteractionPoints[i].Name;
                i++;
            }

            Frames = new FrameContainer[frames.Length];
            i = 0;

            foreach(Frame f in frames)
            {
                Frames[i] = new FrameContainer();
                Frames[i].ToFrameContainer(f, hbs, ips);
                i++;
            }

            Animations = new AnimationContainer[animations.Length];

            for (i = 0; i < animations.Length; i++)
            {
                Animations[i] = new AnimationContainer();
                Animations[i].ToAnimationContainer(animations[i]);
            }
        }

        public void Serialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProjectContainer));
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static ProjectContainer Deserialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(ProjectContainer));
            FileStream fs = new FileStream(path, FileMode.Open);
            ProjectContainer obj =
                (ProjectContainer)serializer.Deserialize(fs);
            fs.Close();
            return obj;
        }

        public Animation[] GetAnimations(Frame[] frames)
        {
            if (Animations == null || Animations.Length <= 0)
                return new Animation[0];
            Animation[] animations = new Animation[Animations.Length];

            for (int i = 0; i < animations.Length; i++)
            {
                animations[i] = Animations[i].ToAnimation(frames);
            }

            return animations;
        }
        string startNumbsPattern = @"^\d+_";
        public Frame[] GetFrames(Tile[,] t16SP12, Tile[,] t16SP34, Tile[,] t8SP12, Tile[,] t8SP34)
        {
            int l = 0;
            if (Hitboxes != null) l = Hitboxes.Length;
            HitBox[] hbs = new HitBox[l];

            for (int i = 0; i < l; i++)
            {
                hbs[i] = Hitboxes[i].ToHitBox();
            }

            l = 0;
            if (InteractionPoints != null) l = InteractionPoints.Length;
            InteractionPoint[] ips = new InteractionPoint[l];

            for (int i = 0; i < l; i++)
            {
                ips[i] = (InteractionPoint)InteractionPoints[i].ToHitBox();
            }

            Frame[] frs = new Frame[Frames.Length];

            for (int i = 0; i < Frames.Length; i++)
            {
                frs[i] = new Frame()
                {
                    MidX = Frames[i].MidX,
                    MidY = Frames[i].MidY,
                    Name = Frames[i].Name
                };
                for (int j = 0; j < Frames[i].TileMasks.Length; j++) 
                {
                    frs[i].AddTile(Frames[i].TileMasks[j].ToTileMask(t16SP12, t16SP34, t8SP12, t8SP34));
                }
                for (int j = 0; j < Frames[i].HitboxesNames.Length; j++)
                {
                    for (int q = 0; q < hbs.Length; q++)
                    {
                        if(hbs[q].Name == Frames[i].HitboxesNames[j])
                        {
                            frs[i].HitBoxes.Add(hbs[q]);
                        }
                    }
                }
                for (int j = 0; j < Frames[i].InteractionPointsNames.Length; j++)
                {
                    for (int q = 0; q < ips.Length; q++)
                    {
                        if (ips[q].Name == Frames[i].InteractionPointsNames[j])
                        {
                            frs[i].InteractionPoints.Add(ips[q]);
                        }
                    }
                }
            }

            Match m;

            foreach(HitBox hb in hbs)
            {
                m = Regex.Match(hb.Name, startNumbsPattern);
                hb.Name = hb.Name.Substring(m.Length);
            }

            foreach (HitBox hb in ips)
            {
                m = Regex.Match(hb.Name, startNumbsPattern);
                hb.Name = hb.Name.Substring(m.Length);
            }

            return frs;
        }
    }
}
