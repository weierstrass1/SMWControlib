namespace SMWControlibBackend.Interaction
{
    public class RectangleHitboxContainer : HitboxContainer
    {
        public int Width, Height;

        public override HitBox ToHitBox()
        {
            RectangleHitBox hb = (RectangleHitBox)base.ToHitBox();
            hb.Width = Width;
            hb.Height = Height;
            return hb;
        }
        public override void ToHitboxContainer(HitBox hb)
        {
            RectangleHitBox rhb = ((RectangleHitBox)hb);
            base.ToHitboxContainer(rhb);
            Width = rhb.Width;
            Height = rhb.Height;
        }
    }
}
