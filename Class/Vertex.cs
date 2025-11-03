namespace DoThi.Class
{
    [Serializable]
    public class Vertex
    {
        public PointF Location { get; }
        public string Value { get; }

        public Vertex(PointF location, string value)
        {
            Location = location;
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vertex other)
            {
                return Location.Equals(other.Location) && Value == other.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Location, Value);
        }

        public override string ToString()
        {
            return $"{Value} ({Location.X}, {Location.Y})";
        }
    }
}
