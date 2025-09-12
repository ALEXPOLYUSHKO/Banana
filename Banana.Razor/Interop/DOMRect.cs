namespace Banana.Razor.Interop
{
    public record class DOMRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public static DOMRect Empty => new()
        {
            X = 0,
            Y = 0,
            Width = 0,
            Height = 0
        };
    }
}
