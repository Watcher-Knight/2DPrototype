using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringFormatter
{
	public static string ToTitleCase(this string text)
	{
		if (text != "")
		{
			text = Regex.Replace(text, @"_", " "); // Remove underscores
			text = Regex.Replace(text, @"^\s+|\s+$", ""); // Remove deadspace
			text = Regex.Replace(text, @"\s+", " "); // Remove multiple adjacent spaces
			text = CapitalizeFirst(text); // Capitalize First Letter
			text = Regex.Replace(text, @"(\S)([A-Z])", "$1 $2"); // Add space before capital letters
			text = Regex.Replace(text, @"(\D)(\d)", "$1 $2"); // Add space between digits and non-digits
			text = string.Join(' ', text.Split(" ").Select(t => t.CapitalizeFirst())); // Capitalize every letter after a space
		}

		return text;
	}

	public static string CapitalizeFirst(this string text)
	{
		if (text != null && text.Length > 0) return text[..1].ToUpper() + text[1..];
		return "";
	}

	public static string Unique(this string text, string[] options)
	{
		int currentNumber = 0;
		while (true)
		{
			string numberText = currentNumber == 0 ? "" : currentNumber.ToString();
			if (!options.Contains(text + numberText)) return text + numberText;
			currentNumber++;
		}
	}
}