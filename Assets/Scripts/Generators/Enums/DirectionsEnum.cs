namespace Generators.Enums
{
    public class DirectionsEnum
    {
        public static readonly DirectionsEnum Top = new(0, 1, 2, "Top");
        public static readonly DirectionsEnum Bottom = new (0, -1, 0, "Bottom");
        public static readonly DirectionsEnum Left = new (-1, 0, 1, "Left"); 
        public static readonly DirectionsEnum Right = new (1, 0, 3, "Right");

        private static readonly DirectionsEnum[] Enums = { Top, Right, Bottom, Left };
    
        private readonly int _xDelta;
        private readonly int _yDelta;
        private readonly int _opposite;
        public readonly string Name;
        public DirectionsEnum Opposite => Enums[_opposite];

        private DirectionsEnum(int xDelta, int yDelta, int opposite, string name)
        {
            _xDelta = xDelta;
            _yDelta = yDelta;
            _opposite = opposite;
            Name = name;
        }

        public (int, int) MovePoint((int, int) point)
        {
            return (point.Item1 - _yDelta, point.Item2 + _xDelta);
        }
    }   
}