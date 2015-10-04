using System.Collections.Generic;
public class Grid {
	private readonly int[,] values;

	public Grid(int[,] values) {
		this.values = values;
	}
	
	// Get all segments for a given tag value.
	public List<Rectangle> getSegmentsForTag (int value) {
		List<Rectangle> segments = getHorizontalSegmentsForTag(value);
		segments.AddRange(getVerticalSegmentsForTag(value));
		
		return segments;
	}
	
	// Get all horizontal segments for the value
	public List<Rectangle> getHorizontalSegmentsForTag (int tag) {
		List<Rectangle> segments = new List<Rectangle>();
		Rectangle segment = null;
		for (int y = 0; y < values.GetLength(1); y++) {
			for (int x = 0; x < values.GetLength(0); x++) {
				int value = values[x, y];
				if (value == tag) {
					// start a new segment
					if (segment == null) {
						Point here = new Point(x, y);
						segment = new Rectangle(here, 1, 1, tag);
					}
					// extend current segment
					else {
						segment.Width++;
					}
				}
				else {
					// end the current segment
					if (segment != null) {
						segments.Add(segment);
						segment = null;
					}
				}
			}
		}
		
		// Add the last segment if there is one.
		if (segment != null) {
			segments.Add(segment);
		}
		
		return segments;
	}
	
	// Get all vertical segments for the value
	public List<Rectangle> getVerticalSegmentsForTag (int tag) {
		List<Rectangle> segments = new List<Rectangle>();
		Rectangle segment = null;
		for (int x = 0; x < values.GetLength(0); x++) {
			for (int y = 0; y < values.GetLength(1); y++) {
				int value = values[x, y];
				if (value == tag) {
					// start a new segment
					if (segment == null) {
						Point here = new Point(x, y);
						segment = new Rectangle(here, 1, 1, tag);
					}
					// extend current segment
					else {
						segment.Height++;
					}
				}
				else {
					// end the current segment
					if (segment != null) {
						segments.Add(segment);
						segment = null;
					}
				}
			}
		}
		
		// Add the last segment if there is one.
		if (segment != null) {
			segments.Add(segment);
		}
		
		return segments;
	}
}

