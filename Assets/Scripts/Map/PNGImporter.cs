using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;
using Hjg.Pngcs;

namespace AssemblyCSharp {
public class PNGImporter : MonoBehaviour {

	public string dictionaryFile;
	public string mapFile;
	
	public string[,] getMatrix () {
		return buildMatrix(dictionaryFile, mapFile);
	}
		
	public static string[,] buildMatrix (string dictionaryPath, string mapPath) {
		var dictionary = readDictionaryFile(dictionaryPath);
		PngReader reader = FileHelper.CreatePngReader(mapPath);
		
		return buildMatrix(dictionary, reader);
	}
	
	public static string[,] buildMatrix (Stream dictionaryStream, Stream mapStream) {
		var dictionary = readDictionary(dictionaryStream);
		PngReader reader = new PngReader(mapStream);
		
		return buildMatrix(dictionary, reader);
	}
	
	public static string[,] buildMatrix (Dictionary<Color, string> dictionary, PngReader reader) {
		ImageInfo info = reader.ImgInfo;
		var matrix = new string[info.Cols, info.Rows];
		
		for (int i = 0; i < info.Rows; i++) {
			ImageLine line = reader.ReadRow(i);
			int[] lineInts = line.Scanline;
			
			for (int j = 0, col = 0; j < lineInts.Length; j += info.Channels, col++) {
				int red = lineInts[j];
				int green = lineInts[j + 1];
				int blue = lineInts[j + 2];
				int alpha = 255;
				
				if (info.Channels == 4) {
					alpha = lineInts[j + 3];
				}
				
				Color color = new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
				string val;
				if (dictionary.TryGetValue(color, out val)) {
					matrix[col, i] = val;
				}
			}
		}
			
		return matrix;
	}
	
	private static Color colorFromRGBA8 (int argb8) {
		var red = (argb8 & 0xFF000000) >> 24;
		var green = (argb8 & 0x00FF0000) >> 16;
		var blue = (argb8 & 0x0000FF00) >> 8;
		var alpha = argb8 & 0x000000FF;
		
		return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
	}
	
	private static Color colorFromARGB8 (uint argb8) {
		var alpha = (argb8 & 0xFF000000) >> 24;
		var red = (argb8 & 0x00FF0000) >> 16;
		var green = (argb8 & 0x0000FF00) >> 8;
		var blue = argb8 & 0x000000FF;
			
		Color result = new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		return result;
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
	private static Dictionary<Color, string> readDictionaryFile (string dictionaryFilename) {
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
	private static Dictionary<Color, string> readDictionary (Stream stream) {
		var reader = new StreamReader(stream, Encoding.ASCII);
		var dictionary = new Dictionary<Color, string>();
			
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
	private static Dictionary<Color, string> readDictionary (string dictionaryContents) {
		MemoryStream stream = new MemoryStream();
		StreamWriter writer = new StreamWriter(stream);
		writer.Write(dictionaryContents);
		writer.Flush();
		stream.Position = 0;
		return readDictionary(stream);
	}
		
	private static Color colorFromString (string color) {
		color = color.Trim();
		color = color.ToLowerInvariant();
		uint colorInt;
		
		if (color.StartsWith("0x")) {
			color = color.Substring(2);
			colorInt = Convert.ToUInt32(color, 16);
		}
		else if (color.StartsWith("0b")) {
			color = color.Substring(2);
			colorInt = Convert.ToUInt32(color, 2);
		}
		else {
			colorInt = Convert.ToUInt32(color, 10);
		}
		
		return colorFromARGB8(colorInt);
	}
}
}
