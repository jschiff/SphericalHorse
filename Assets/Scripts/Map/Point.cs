public struct Point {
	public readonly int X;
	public readonly int Y;
	public static Point ZERO = new Point(0, 0);
	
	public Point(int x, int y) {
		this.X = x;
		this.Y = y;
	}
	
	public Point(Point source) {
		this.X = source.X;
		this.Y = source.Y;
	}
	
	public static Point operator + (Point point) {
		return new Point(point);
	}
	
	public static Point operator - (Point point) {
		return new Point(-point.X, -point.Y);
	}
	
	public static Point operator - (Point a, Point b) {
		return a.Subtract(b);
	}
	
	public static Point operator + (Point a, Point b) {
		return a.Add(b);
	}
	
	public static bool operator == (Point a, Point b) {
		return (a.X == b.X && a.Y == b.Y);
	}
	
	public static bool operator != (Point a, Point b) {
		return (a.X != b.X || a.Y != b.Y);
	}
	
	public Point Add (Point other) {
		return new Point(X + other.X, Y + other.Y);
	}
	
	public Point Subtract (Point other) {
		return Add(-other);
	}
	
	public Point Add (int x, int y) {
		return new Point(x + X, y + Y);
	}
	
	public Point Subtract (int x, int y) {
		return new Point(X - x, Y - y);
	}
}

