// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
public class Rectangle {
	public Point TopLeft;
	public Point BottomRight {
		get { 
			return TopLeft.Add(Width, Height);
		}
	}
	public bool Vertical;
	public bool Horizontal { 
		get { return !this.Vertical;}
		set { this.Vertical = !value;}
	}
	public int Height;
	public int Width;
	public int TagValue;
	
	public Rectangle(Point topLeft, int width, int height, int tagValue) {
		this.TopLeft = topLeft;
		this.Width = width;
		this.Height = height;
		this.TagValue = tagValue;
	}
}

