namespace Đồ_Thị.Class
{
    [Serializable]
    public class Vertex(PointF location, string value)
    {
        public PointF Location { get; set; } = location;
        public string Value { get; set; } = value;

        // Override Equals và GetHashCode để so sánh các đỉnh dễ dàng hơn
        public override bool Equals(object obj)
        {
            if (obj is Vertex other)
            {
                // Custom comparison for PointF
                return Location.X == other.Location.X &&
                       Location.Y == other.Location.Y &&
                       Value == other.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Ensure the hash code considers the floating-point precision
            return Location.X.GetHashCode() ^ Location.Y.GetHashCode() ^ Value.GetHashCode();
        }
    }
}
