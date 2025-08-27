namespace Banana.Razor.Interop
{
    public readonly record struct DOMSize
    {
        public DOMSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; init; }
        public int Height { get; init; }
    }
}
