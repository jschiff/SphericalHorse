using UnityEngine;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace AssemblyCSharp {
public class PNGImporter {

	public static void main (string[] args) {
		var dictionary = readDictionaryFile(@"/Users/jschiff/workspace/SphericalHorse/Assets/colormap.map");
		System.Console.WriteLine(dictionary);
	}

	public static string[,] buildMatrix (string dictionaryPath, string mapPath) {
		var dictionary = readDictionaryFile(dictionaryPath);
		var bitmap = new Bitmap(mapPath);
		
		return buildMatrix(dictionary, bitmap);
	}
	
	public static string[,] buildMatrix (Stream dictionaryStream, Stream mapStream) {
		var dictionary = readDictionary(dictionaryStream);
		var bitmap = new Bitmap(mapStream);
		
		return buildMatrix(dictionary, bitmap);
	}
	
	public static string[,] buildMatrix (Dictionary<System.Drawing.Color, string> dictionary, Bitmap bitmap) {
		var matrix = new string[bitmap.Width, bitmap.Height];
			
		for (int i = 0; i < bitmap.Width; i++) {
			for (int j = 0; j < bitmap.Height; j++) {
				var color = bitmap.GetPixel(i, j);
				matrix[i, j] = dictionary[color];
			}
		}
			
		return matrix;
	}
	
	/**
	 * Dictionary  format is:
	 * ASCII Encoded
	 * One entry per line.
	 * Comment a line by prefixing it with '#'
	 * Each line should be a hexadecimal 8 byte number, followed by a colon, followed by a second hexadecimal number.
	 * The first number is a 32 bit color with 8 bits each for Alpha, Red, Green, and Blue respectively.
	 * The second number is an id to refer to a prefab.
	 */
	private static Dictionary<System.Drawing.Color, string> readDictionaryFile (string dictionaryFilename) {
		var fStream = File.OpenRead(dictionaryFilename);
		return readDictionary(fStream);
	}
		
	/**
	 * Dictionary  format is:
	 * ASCII Encoded
	 * One entry per line.
	 * Comment a line by prefixing it with '#'
	 * Each line should be a hexadecimal 8 byte number, followed by a colon, followed by a second hexadecimal number.
	 * The first number is a 32 bit color with 8 bits each for Alpha, Red, Green, and Blue respectively.
	 * The second number is an id to refer to a prefab.
	 */
	private static Dictionary<System.Drawing.Color, string> readDictionary (Stream stream) {
		var reader = new StreamReader(stream, Encoding.ASCII);
		var dictionary = new Dictionary<System.Drawing.Color, string>();
			
		int lineNum = -1;
		string line = null;
		while (null != (line = reader.ReadLine())) {
			lineNum++;
				
			line = line.Trim();
			// Ignore comments
			int commentStart = line.IndexOf('#');
			if (commentStart > -1) {
				line = line.Substring(0, commentStart);
			}
				
			var tokens = line.Split(':');
			if (tokens.Length == 2) {
				var color = colorFromString(tokens[0]);
				dictionary[color] = tokens[1];
			}
				else {
				throw new IOException("Dictionary format incorrect at line " + lineNum);
			}
		}
			
		return dictionary;
	}
	
	/**
	 * Dictionary  format is:
	 * ASCII Encoded
	 * One entry per line.
	 * Comment a line by prefixing it with '#'
	 * Each line should be a hexadecimal 8 byte number, followed by a colon, followed by a second hexadecimal number.
	 * The first number is a 32 bit color with 8 bits each for Alpha, Red, Green, and Blue respectively.
	 * The second number is an id to refer to a prefab.
	 */
	private static Dictionary<System.Drawing.Color, string> readDictionary (string dictionaryContents) {
		MemoryStream stream = new MemoryStream();
		StreamWriter writer = new StreamWriter(stream);
		writer.Write(dictionaryContents);
		writer.Flush();
		stream.Position = 0;
		return readDictionary(stream);
	}
		
	private static System.Drawing.Color colorFromString (string color) {
		int colorInt = int.Parse(color);
		var alpha = (int)(colorInt | 0xFF000000) >> 6;
		var red = (int)(colorInt | 0x00FF0000) >> 4;
		var green = (int)(colorInt | 0x0000FF00) >> 2;
		var blue = (int)colorInt | 0x000000FF;
		return System.Drawing.Color.FromArgb(alpha, red, green, blue);
	}
}
}
